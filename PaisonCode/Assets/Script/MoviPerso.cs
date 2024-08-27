using UnityEngine;
using System.Collections;

public class MovimentoPersonagem : MonoBehaviour
{
    public float velocidade = 5.0f;
    public float alturaSubida = 1.0f;
    private Animator anim;
    public bool andandoX;
    public bool andandoY;
    public bool andando;

    private bool isPushing = false;
    public float pushCooldown = 1.0f;
    private float lastPushTime = 0f;
    public LayerMask pushableLayerMask;
    public LayerMask climbableLayerMask;

    private bool isClimbing = false;
    private Vector2 posicaoInicial;
    private Transform localDeSubida;

    public bool isDead = false; // Variável que indica se o personagem está morto
    private Vector2 lastAntesDaMortePosition; // Última posição do objeto AntesDaMorte que o personagem passou

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isClimbing)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                DescendObject();
            }
            return;
        }

        if (!isPushing)
        {
            HandleMovement();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isClimbing)
            {
                TryClimbObject();
            }
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            verticalInput = 0f;
        }
        else
        {
            horizontalInput = 0f;
        }

        anim.SetBool("EmMovimento", horizontalInput != 0 || verticalInput != 0);
        anim.SetBool("EmMovimentoX", horizontalInput != 0);
        anim.SetBool("EmMovimentoY", verticalInput != 0);

        if (velocidade > 0)
        {
            if (Input.GetKey(KeyCode.A) && !andando && !isDead)
            {
                anim.SetFloat("MovimentoX", -1);
                andando = true;
                TryPushObject(Vector2.left);
            }
            if (Input.GetKey(KeyCode.S) && !andando && !isDead)
            {
                anim.SetFloat("MovimentoY", -1);
                andando = true;
                TryPushObject(Vector2.down);
            }
            if (Input.GetKey(KeyCode.D) && !andando && !isDead)
            {
                anim.SetFloat("MovimentoX", 1);
                andando = true;
                TryPushObject(Vector2.right);
            }
            if (Input.GetKey(KeyCode.W) && !andando && !isDead)
            {
                anim.SetFloat("MovimentoY", 1);
                andando = true;
                TryPushObject(Vector2.up);
            }

            if (Input.GetKeyUp(KeyCode.A) && !isDead)
            {
                anim.SetFloat("MovimentoX", 0);
                andando = false;
            }
            if (Input.GetKeyUp(KeyCode.S) && !isDead)
            {
                anim.SetFloat("MovimentoY", 0);
                andando = false;
            }
            if (Input.GetKeyUp(KeyCode.D) && !isDead)
            {
                anim.SetFloat("MovimentoX", 0);
                andando = false;
            }
            if (Input.GetKeyUp(KeyCode.W) && !isDead)
            {
                anim.SetFloat("MovimentoY", 0);
                andando = false;
            }
        }

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
        if (velocidade > 0)
        {
            Move(direction);
        }
    }

    void Move(Vector2 direction)
    {
        Vector2 movement = direction * velocidade * Time.deltaTime;
        transform.Translate(movement);
    }

    void TryPushObject(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position + direction * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.5f, pushableLayerMask);

        Debug.DrawRay(origin, direction * 1f, Color.red, 2f);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Empurravel"))
            {
                ObjetoEmpurravel objEmpurravel = hit.collider.GetComponent<ObjetoEmpurravel>();
                if (objEmpurravel != null && objEmpurravel.CanBePushed)
                {
                    if (CanMoveObject(hit.collider.gameObject, direction))
                    {
                        isPushing = true;
                        lastPushTime = Time.time;
                        Vector2 targetPosition = (Vector2)hit.collider.transform.position + direction;
                        StartCoroutine(PushObject(hit.collider.gameObject, targetPosition));
                    }
                }
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    bool CanMoveObject(GameObject obj, Vector2 direction)
    {
        Vector2 currentPosition = obj.transform.position;
        Vector2 newPosition = currentPosition + direction;

        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogWarning("Collider2D not found on " + obj.name);
            return false;
        }

        return !Physics2D.OverlapBox(newPosition, collider.bounds.size, 0f, LayerMask.GetMask("Default"));
    }

    IEnumerator PushObject(GameObject obj, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = obj.transform.position;

        while (elapsedTime < pushCooldown)
        {
            obj.transform.position = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / pushCooldown));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
        isPushing = false;
    }

    void TryClimbObject()
    {
        Vector2 origin = transform.position;
        Collider2D hit = Physics2D.OverlapCircle(origin, 1f, climbableLayerMask);

        if (hit != null && hit.CompareTag("Subivel"))
        {
            ClimbObject(hit.gameObject);
        }
    }

    void ClimbObject(GameObject climbable)
    {
        isClimbing = true;
        anim.SetBool("Subindo", true);
        posicaoInicial = transform.position;
        Transform localDeSubidaTransform = climbable.transform.Find("LocalDeSubida");
        if (localDeSubidaTransform != null)
        {
            localDeSubida = localDeSubidaTransform;
            transform.position = localDeSubida.position;
        }
        else
        {
            Debug.LogError("LocalDeSubida não encontrado no objeto escalável.");
        }
        GetComponent<Rigidbody2D>().isKinematic = true;
        velocidade = 0;
    }

    void DescendObject()
    {
        if (localDeSubida != null)
        {
            isClimbing = false;
            anim.SetBool("Subindo", false);
            Vector2 descendPosition = posicaoInicial;
            transform.position = new Vector2(descendPosition.x, localDeSubida.position.y - alturaSubida);
            GetComponent<Rigidbody2D>().isKinematic = false;
            velocidade = 5.0f;
            localDeSubida = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Queda"))
        {
            isDead = true;
            velocidade = 0;
            
            // Salva a posição atual no caso de retorno
            StartCoroutine(ReturnToLastAntesDaMortePosition());
        }
        else if (other.CompareTag("AntesDaMorte"))
        {
            // Atualiza a posição do último "AntesDaMorte" que o personagem passou
            lastAntesDaMortePosition = other.transform.position;
        }
    }

    private IEnumerator ReturnToLastAntesDaMortePosition()
    {
        // Aguarda 3 segundos
        yield return new WaitForSecondsRealtime(3);

        // Volta para a última posição de AntesDaMorte
        if (lastAntesDaMortePosition != Vector2.zero)
        {
            transform.position = lastAntesDaMortePosition;
            lastAntesDaMortePosition = Vector2.zero; // Limpa a posição após o retorno
        }

        isDead = false; // Permite o movimento novamente
        velocidade = 5;
    }
}
