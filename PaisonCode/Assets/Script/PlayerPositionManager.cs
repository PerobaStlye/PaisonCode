using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    private Vector2 savedPosition;
    private bool isSaved = false;
    private bool isPaused = false;

    void Start()
    {
        // Garante que o PlayerPositionManager seja reseta ao iniciar o jogo
        Reset();
    }

    public void SaveInitialPosition(Vector2 position)
    {
        savedPosition = position;
        isSaved = true;
        PauseTime(); // Pausa o tempo quando a posição é salva
    }

    public void RestorePosition()
    {
        if (isSaved)
        {
            // Restaura a posição salva
            FindObjectOfType<MovimentoPersonagem>().transform.position = savedPosition;
            isSaved = false;
            ResumeTime(); // Retoma o tempo quando a posição é restaurada
        }
    }

    private void PauseTime()
    {
        if (!isPaused)
        {
            Time.timeScale = 0; // Pausa o tempo
            isPaused = true;
        }
    }

    private void ResumeTime()
    {
        if (isPaused)
        {
            Time.timeScale = 1; // Retoma o tempo
            isPaused = false;
        }
    }

    void Reset()
    {
        // Garante que o tempo seja retomado quando o jogo for fechado e reiniciado
        Time.timeScale = 1;
        isPaused = false;
        isSaved = false;
    }

    private void OnApplicationQuit()
    {
        Reset();
    }
}
