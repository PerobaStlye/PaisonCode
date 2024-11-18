using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public string tagCorreta;
    public int maxTentativas = 3; // Número máximo de tentativas permitido
    private int tentativasAtuais = 0; // Contador de tentativas realizadas
    private bool estaSendoArrastado = false;
    private bool foiColocado = false;
    private bool correta = false;
    private Vector3 posicaoInicialCena;
    private float profundidadeOriginal;
    private Collider2D triggerAtual;

    // Referências diretas para os objetos no Inspector
    public GameObject ativarEsseObjeto; // Objeto a ser ativado
    public GameObject desativarEsseObjeto; // Objeto a ser desativado

    private void Start()
    {
        profundidadeOriginal = transform.position.z;
        posicaoInicialCena = transform.position;

        // Garante que o Collider2D começa como trigger
        GetComponent<Collider2D>().isTrigger = true;
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
        }
    }

    private void OnMouseUp()
    {
        estaSendoArrastado = false;

        if (!foiColocado && !correta)
        {
            tentativasAtuais++;
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

            foiColocado = true;
            correta = true;

            // Ativa/desativa os objetos especificados
            GerenciarObjetosCena(true);

            // Desativa o trigger temporariamente ao colocar no chão
            GetComponent<Collider2D>().isTrigger = false;

            // Reativa o trigger após 0,1 segundo
            Invoke("AtivarTrigger", 0.1f);
        }
        else
        {
            if (tentativasAtuais >= maxTentativas)
            {
                ResetPosition();
            }
        }
    }

    private void AtivarTrigger()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void ResetPosition()
    {
        transform.position = posicaoInicialCena;
        foiColocado = false;
        correta = false;
        tentativasAtuais = 0; // Reseta as tentativas
        GetComponent<SpriteRenderer>().color = Color.white;

        // Garante que o Collider2D volte a ser trigger
        GetComponent<Collider2D>().isTrigger = true;

        // Ativa/desativa os objetos especificados caso tenha sido resetado
        GerenciarObjetosCena(false);
    }

    private void GerenciarObjetosCena(bool ativar)
    {
        if (ativarEsseObjeto != null)
        {
            ativarEsseObjeto.SetActive(ativar);
        }
        else
        {
            Debug.LogWarning("Objeto para ativar não está atribuído!");
        }

        if (desativarEsseObjeto != null)
        {
            desativarEsseObjeto.SetActive(!ativar);
        }
        else
        {
            Debug.LogWarning("Objeto para desativar não está atribuído!");
        }
    }
}
