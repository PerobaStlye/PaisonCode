using UnityEngine;

public class ScriptTelas : MonoBehaviour
{
    public int pontuacaoAtual;
    public int pontuacaoAdd;
    public int proxTela;
    public int streak;
    private bool vidaExtra;

    private void Start()
    {
        // Inicializa PlayerPrefs
        PlayerPrefs.SetInt("PontuaçãoTelas", 0);
        PlayerPrefs.SetInt("vidaExtra", 0);
        PlayerPrefs.Save();
        streak = 0; // Inicializa o streak
        vidaExtra = PlayerPrefs.GetInt("vidaExtra") == 1; // Recupera o estado da vida extra
    }

    private void Update()
    {
        pontuacaoAtual = PlayerPrefs.GetInt("PontuaçãoTelas");

        // Quando o jogador acerta o puzzle
        if (pontuacaoAtual == 1)
        {
            pontuacaoAdd++;
            streak++;
            pontuacaoAtual = 0;
            PlayerPrefs.SetInt("PontuaçãoTelas", 0);
            Debug.Log($"Streak Atual: {streak}");
        }

        // O jogador ganha uma vida extra após acertar 3 puzzles seguidos
        if (streak == 3)
        {
            vidaExtra = true;
            PlayerPrefs.SetInt("vidaExtra", 1);
            PlayerPrefs.Save();
            streak = 0;
            Debug.Log("Vida Extra Ativada!");
        }

        // Condição para avançar para a próxima tela
        if (pontuacaoAdd == 3)
        {
            pontuacaoAdd = 0;
            GameManager.Instance.ChangeScreen($"Puzzle{proxTela}");
        }
    }

    // Método chamado quando o jogador erra o puzzle
    public void OnBlocoErro(MouseMover bloco)
    {
        Debug.Log("Erro no Bloco Detectado!");

        vidaExtra = PlayerPrefs.GetInt("vidaExtra") == 1;

        if (vidaExtra)
        {
            vidaExtra = false;
            PlayerPrefs.SetInt("vidaExtra", 0);
            PlayerPrefs.Save();
            Debug.Log("Vida Extra Usada!");
            bloco.ResetPosition(true);  // Apenas o bloco incorreto volta à posição inicial
        }
        else
        {
            pontuacaoAdd = 0;
            ResetarPuzzle();  // Reseta todos os blocos manualmente
        }
    }

    // Reseta todos os blocos do puzzle para o estado inicial
    private void ResetarPuzzle()
    {
        MouseMover[] blocos = FindObjectsOfType<MouseMover>();
        foreach (MouseMover bloco in blocos)
        {
            bloco.ResetPosition(false);  // Reseta todos os blocos
        }

        Debug.Log("Puzzle resetado!");
    }
}
