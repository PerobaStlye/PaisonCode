using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public string tagCorreta;
    private bool estaSendoArrastado = false;
    private bool foiColocado = false;
    private bool correta = false;
    private Vector3 posicaoInicialCena;
    private float profundidadeOriginal;
    private Collider2D triggerAtual;

    private void Start()
    {
        profundidadeOriginal = transform.position.z;
        posicaoInicialCena = transform.position;
    }

    private void Update()
    {
        if (estaSendoArrastado)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = profundidadeOriginal;
            transform.position = mousePos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagCorreta))
        {
            triggerAtual = other;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(tagCorreta))
        {
            triggerAtual = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == triggerAtual)
        {
            triggerAtual = null;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !foiColocado && !correta)
        {
            estaSendoArrastado = true;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnMouseUp()
    {
        estaSendoArrastado = false;
        GetComponent<Collider2D>().isTrigger = false;

        if (!foiColocado && !correta)
        {
            Invoke("ChecarPosicao", 0.2f);
        }
    }

    private void ChecarPosicao()
    {
        if (triggerAtual != null && !foiColocado)
        {
            BoxCollider2D boxCollider = triggerAtual.GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                Vector3 centroDoCollider = triggerAtual.transform.position + (Vector3)boxCollider.offset;
                transform.position = centroDoCollider;
            }
            else
            {
                transform.position = triggerAtual.transform.position;
            }

            GetComponent<SpriteRenderer>().color = Color.green;
            foiColocado = true;
            correta = true;
            PlayerPrefs.SetInt("PontuaçãoTelas", 1);
            PlayerPrefs.Save();
        }
        else
        {
            ResetPosition(true);
            GameObject.FindObjectOfType<ScriptTelas>().OnBlocoErro(this);
        }
    }

    public void ResetPosition(bool apenasBloco)
    {
        transform.position = posicaoInicialCena;
        foiColocado = false;
        correta = false;
        GetComponent<SpriteRenderer>().color = Color.white;

        // Se apenas o bloco deve ser reposicionado, podemos adicionar aqui
        if (!apenasBloco)
        {
            // Lógica adicional, se necessário
        }
    }
}
