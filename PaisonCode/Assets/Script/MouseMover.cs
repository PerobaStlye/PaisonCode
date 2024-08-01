using UnityEngine;

public class MouseMover : MonoBehaviour
{
    public string tagCorreta;
    private bool estaSendoArrastado = false;
    private bool foiColocado = false;
    private bool correta = false;
    private Vector3 posicaoInicial;
    private Vector3 posicaoInicialCena;
    private float profundidadeOriginal;

    private void Start()
    {
        profundidadeOriginal = transform.position.z;
        posicaoInicialCena = transform.position; // Salva a posi��o inicial no in�cio da cena
    }

    private void Update()
    {
        if (estaSendoArrastado && !foiColocado)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = profundidadeOriginal;
            transform.position = mousePos + posicaoInicial;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!estaSendoArrastado && other.CompareTag(tagCorreta))
        {
            if (!foiColocado)
            {
                // Obt�m o centro do BoxCollider2D do outro objeto
                BoxCollider2D boxCollider = other.GetComponent<BoxCollider2D>();
                if (boxCollider != null)
                {
                    Vector3 centroDoCollider = other.transform.position + (Vector3)boxCollider.offset;
                    transform.position = centroDoCollider;
                }
                else
                {
                    transform.position = other.transform.position;
                }

                if (gameObject.CompareTag("Movel") && other.CompareTag(tagCorreta))
                {
                    GetComponent<SpriteRenderer>().color = Color.green;
                    foiColocado = true;
                    correta = true;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !foiColocado && !correta)
        {
            estaSendoArrastado = true;
            posicaoInicial = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.GetComponent<Collider2D>().isTrigger = true; // Torna o collider um trigger enquanto arrasta
        }
    }

    private void OnMouseUp()
    {
        if (estaSendoArrastado && !foiColocado && !correta)
        {
            Invoke("RetornarParaPosicaoInicial", 0.5f); // Chama o m�todo RetornarParaPosicaoInicial ap�s 0.5 segundos
        }
        estaSendoArrastado = false;
        gameObject.GetComponent<Collider2D>().isTrigger = false; // Volta o collider ao estado normal quando solta
    }

    private void RetornarParaPosicaoInicial()
    {
        if (!foiColocado && !correta)
        {
            transform.position = posicaoInicialCena; // Volta para a posi��o inicial da cena se n�o foi colocado corretamente
        }
    }
}
