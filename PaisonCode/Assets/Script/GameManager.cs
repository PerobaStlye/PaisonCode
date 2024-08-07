using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player; // Referência ao objeto player
    public Vector2 personagemPosicao;
    public int moedasColetadas;
    public int outrosItens;
    public bool jogadorEstaSubindo;

    private void Awake()
    {
        // Garante que haja apenas uma instância do GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Não destrói o GameManager ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Remove o GameManager duplicado
        }
    }

    private void Start()
    {
        // Inicia a corrotina para mostrar a posição do jogador
        if (player != null)
        {
            StartCoroutine(MostrarPosicaoJogador());
        }
        else
        {
            Debug.LogWarning("Referência ao player não foi atribuída no GameManager.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Atualiza a posição do personagem no GameManager
            personagemPosicao = player.transform.position;
        }
    }

    private System.Collections.IEnumerator MostrarPosicaoJogador()
    {
        while (true)
        {
            if (player != null)
            {
                // Atualiza a posição do personagem e exibe no console
                personagemPosicao = player.transform.position;
                Debug.Log("Posição do Jogador: " + personagemPosicao);
            }
            yield return new WaitForSeconds(1f); // Espera 1 segundo
        }
    }

    // Métodos para manipular variáveis e dados
    public void AtualizarPosicaoPersonagem(Vector2 novaPosicao)
    {
        personagemPosicao = novaPosicao;
    }

    public void AdicionarMoedas(int quantidade)
    {
        moedasColetadas += quantidade;
    }

    public void AdicionarItens(int quantidade)
    {
        outrosItens += quantidade;
    }

    public void DefinirEstadoSubindo(bool estado)
    {
        jogadorEstaSubindo = estado;
    }

    public void SalvarDados()
    {
        // Implementar lógica de salvamento se necessário
    }

    public void CarregarDados()
    {
        // Implementar lógica de carregamento se necessário
    }
}
