using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    public float velocidade = 5.0f; // Velocidade de movimento do personagem.
    private Animator anim; // Referência ao componente Animator.
    public bool andandoX;
    public bool andandoY;
    public bool andando;

    private bool isPushing = false; // Indica se o personagem está empurrando um objeto
    public float pushCooldown = 1.0f; // Tempo necessário para completar um empurrão
    private float lastPushTime = 0f; // Tempo da última vez que o personagem iniciou um empurrão

    public LayerMask pushableLayerMask; // LayerMask para objetos empurráveis

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isPushing && Time.time - lastPushTime < pushCooldown)
        {
            return; // Se está empurrando e o cooldown não terminou, retorna
        }

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

        if (Input.GetKey(KeyCode.A) && !andando)
        {
            anim.SetFloat("MovimentoX", -1);
            andando = true;
            TryPushObject(Vector2.left);
        }
        if (Input.GetKey(KeyCode.S) && !andando)
        {
            anim.SetFloat("MovimentoY", -1);
            andando = true;
            TryPushObject(Vector2.down);
        }
        if (Input.GetKey(KeyCode.D) && !andando)
        {
            anim.SetFloat("MovimentoX", 1);
            andando = true;
            TryPushObject(Vector2.right);
        }
        if (Input.GetKey(KeyCode.W) && !andando)
        {
            anim.SetFloat("MovimentoY", 1);
            andando = true;
            TryPushObject(Vector2.up);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetFloat("MovimentoX", 0);
            andando = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetFloat("MovimentoY", 0);
            andando = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetFloat("MovimentoX", 0);
            andando = false;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("MovimentoY", 0);
            andando = false;
        }

        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 movement = direction * velocidade * Time.deltaTime;
        transform.Translate(movement);
    }

    void TryPushObject(Vector2 direction)
    {
        Vector2 origin = (Vector2)transform.position + direction * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 1f, pushableLayerMask);

        Debug.DrawRay(origin, direction * 1f, Color.red, 2f);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Empurravel"))
            {
                ObjetoEmpurravel objEmpurravel = hit.collider.GetComponent<ObjetoEmpurravel>();
                if (objEmpurravel != null && objEmpurravel.CanBePushed)
                {
                    // Verifique se o objeto pode se mover sem colidir com outros objetos
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
        // Cria um pequeno teste de colisão na nova posição para verificar se o objeto pode se mover
        Vector2 currentPosition = obj.transform.position;
        Vector2 newPosition = currentPosition + direction;

        // Cria um colisor temporário para verificar a colisão
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogWarning("Collider2D not found on " + obj.name);
            return false;
        }

        // Teste de colisão com a nova posição
        return !Physics2D.OverlapBox(newPosition, collider.bounds.size, 0f, LayerMask.GetMask("Default"));
    }

    System.Collections.IEnumerator PushObject(GameObject obj, Vector2 targetPosition)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = obj.transform.position;

        while (elapsedTime < pushCooldown)
        {
            // Move o objeto gradualmente
            obj.transform.position = Vector2.Lerp(startPosition, targetPosition, (elapsedTime / pushCooldown));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition;
        isPushing = false;
    }
}
