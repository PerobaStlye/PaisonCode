using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    public float velocidade = 5.0f; // Velocidade de movimento do personagem.
    public float alturaSubida = 1.0f; // Altura para ajuste quando descer
    private Animator anim; // Referência ao componente Animator.
    public bool andandoX;
    public bool andandoY;
    public bool andando;

    private bool isPushing = false; // Indica se o personagem está empurrando um objeto
    public float pushCooldown = 1.0f; // Tempo necessário para completar um empurrão
    private float lastPushTime = 0f; // Tempo da última vez que o personagem iniciou um empurrão

    public LayerMask pushableLayerMask; // LayerMask para objetos empurráveis
    public LayerMask climbableLayerMask; // LayerMask para objetos escaláveis

    private bool isClimbing = false; // Indica se o personagem está escalando
    private Vector2 posicaoInicial; // Posição inicial do personagem
    private Transform localDeSubida; // Referência ao objeto LocalDeSubida

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isClimbing)
        {
            // Verifica a entrada de descida e desce conforme o ponto selecionado
            if (Input.GetKeyDown(KeyCode.J))
            {
                DescendObject();
            }
            return; // Se está escalando, não permite outras ações
        }

        // Só permite movimentar se não estiver empurrando um objeto
        if (!isPushing)
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
            }

            Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;
            if (velocidade > 0) // Só se move se a velocidade for maior que 0
            {
                Move(direction);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!isClimbing)
            {
                TryClimbObject();
            }
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
        anim.SetBool("Subindo", true); // Define a variável de animação
        // Salva a posição inicial antes de subir
        posicaoInicial = transform.position;
        // Encontra o objeto LocalDeSubida
        Transform localDeSubidaTransform = climbable.transform.Find("LocalDeSubida");
        if (localDeSubidaTransform != null)
        {
            localDeSubida = localDeSubidaTransform;
            // Move o personagem para o LocalDeSubida
            transform.position = localDeSubida.position;
        }
        else
        {
            Debug.LogError("LocalDeSubida não encontrado no objeto escalável.");
        }
        // Desativa a gravidade e outros movimentos
        GetComponent<Rigidbody2D>().isKinematic = true;
        velocidade = 0; // Desativa o movimento enquanto estiver escalando
    }

    void DescendObject()
    {
        if (localDeSubida != null)
        {
            isClimbing = false;
            anim.SetBool("Subindo", false); // Define a variável de animação
            // Move o personagem para a posição abaixo do LocalDeSubida
            Vector2 descendPosition = posicaoInicial;
            transform.position = new Vector2(descendPosition.x, localDeSubida.position.y - alturaSubida);
            // Ativa a gravidade e outros movimentos
            GetComponent<Rigidbody2D>().isKinematic = false;
            velocidade = 5.0f; // Restaura a velocidade original
            localDeSubida = null; // Limpa a referência
        }
    }
}
