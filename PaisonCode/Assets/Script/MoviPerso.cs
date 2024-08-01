using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    public float velocidade = 5.0f; // Velocidade de movimento do personagem.
    private Animator anim; // Referência ao componente Animator.
    public bool andandoX;
    public bool andandoY;
    public bool andando;

        public GameObject tela; // Referência ao objeto da tela
    private bool telaAtiva = false; // Indica se a tela está ativa
    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // Verifica se a tela está ativa e retorna se estiver
        if (telaAtiva)
        {
            // Você pode adicionar lógica adicional aqui se desejar
            // Verifica se a tecla "Esc" foi pressionada e desativa a tela se necessário
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                AtivarDesativarTela();
            }
            return;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Verifica a direção predominante
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // Se a entrada horizontal for mais significativa, limita a vertical
            verticalInput = 0f;
        }
        else
        {
            // Se a entrada vertical for mais significativa, limita a horizontal
            horizontalInput = 0f;
        }

        // Configurações de animação
        anim.SetBool("EmMovimento", horizontalInput != 0 || verticalInput != 0);
        anim.SetBool("EmMovimentoX", horizontalInput != 0);
        anim.SetBool("EmMovimentoY", verticalInput != 0);
        if (Input.GetKey(KeyCode.A) && andando == false)
        {
            anim.SetFloat("MovimentoX", -1);
            andando = true;
        }
        if (Input.GetKey(KeyCode.S) && andando == false)
        {
            anim.SetFloat("MovimentoY", -1);
            andando = true;
        }
        if (Input.GetKey(KeyCode.D) && andando == false)
        {
            anim.SetFloat("MovimentoX", 1);
            andando = true;
        }
        if (Input.GetKey(KeyCode.W) && andando == false)
        {
            anim.SetFloat("MovimentoY", 1);
            andando = true;
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

        if (Input.GetKeyDown(KeyCode.J) && !telaAtiva)
        {
            AtivarDesativarTela();
        }

        // Calcular a direção e normalizar
        Vector2 direction = new Vector2(horizontalInput, verticalInput).normalized;

        // Mover o jogador
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        // Calcular o vetor de movimento
        Vector2 movement = direction * velocidade * Time.deltaTime;

        // Mover o jogador
        transform.Translate(movement);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Porta"))
        {
            // Aqui você pode adicionar a lógica para ativar a tela quando entra em contato com a porta
            AtivarDesativarTela();
        }
    }
    public void AtivarDesativarTela()
    {
        telaAtiva = !telaAtiva;

        // Adicione mensagens de depuração
        Debug.Log("Tela Ativada: " + telaAtiva);

        // Ativa ou desativa o objeto da tela
        tela.SetActive(telaAtiva);

        // Se a tela foi ativada, pare as animações do personagem
        if (telaAtiva)
        {
            anim.SetBool("EmMovimento", false);
            anim.SetBool("EmMovimentoX", false);
            anim.SetBool("EmMovimentoY", false);
            anim.SetFloat("MovimentoX", 0);
            anim.SetFloat("MovimentoY", 0);
            andando = false;
        }
    }
}