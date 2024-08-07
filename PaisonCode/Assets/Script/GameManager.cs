using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player; // Refer�ncia ao objeto player
    public Vector2 personagemPosicao;
    public int moedasColetadas;
    public int outrosItens;
    public bool jogadorEstaSubindo;

    private void Awake()
    {
        // Garante que haja apenas uma inst�ncia do GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // N�o destr�i o GameManager ao trocar de cena
        }
        else
        {
            Destroy(gameObject); // Remove o GameManager duplicado
        }
    }

    private void Start()
    {
        // Inicia a corrotina para mostrar a posi��o do jogador
        if (player != null)
        {
            StartCoroutine(MostrarPosicaoJogador());
        }
        else
        {
            Debug.LogWarning("Refer�ncia ao player n�o foi atribu�da no GameManager.");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Atualiza a posi��o do personagem no GameManager
            personagemPosicao = player.transform.position;
        }
    }

    private System.Collections.IEnumerator MostrarPosicaoJogador()
    {
        while (true)
        {
            if (player != null)
            {
                // Atualiza a posi��o do personagem e exibe no console
                personagemPosicao = player.transform.position;
                Debug.Log("Posi��o do Jogador: " + personagemPosicao);
            }
            yield return new WaitForSeconds(1f); // Espera 1 segundo
        }
    }

    // M�todos para manipular vari�veis e dados
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
        // Implementar l�gica de salvamento se necess�rio
    }

    public void CarregarDados()
    {
        // Implementar l�gica de carregamento se necess�rio
    }
}
