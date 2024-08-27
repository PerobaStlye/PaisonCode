using UnityEngine;
using System.Collections;

public class PlayerPositionManager : MonoBehaviour
{
    private Vector2 savedPosition;
    private bool isSaved = false;
    private bool isPaused = false;
    public MovimentoPersonagem playerScript; // Referência ao script MovimentoPersonagem

    void Start()
    {
        Reset();
    }

    public void SaveInitialPosition(Vector2 position)
    {
        savedPosition = position;
        isSaved = true;
        PauseTime();
        Debug.Log("Posição inicial salva: " + savedPosition);
    }

    public void RestorePosition()
    {
        if (isSaved)
        {
            StartCoroutine(RestoreAfterDelay());
        }
    }

    private IEnumerator RestoreAfterDelay()
    {
        Debug.Log("Iniciando contagem de 3 segundos...");
        yield return new WaitForSecondsRealtime(3); // Aguarda 3 segundos antes de restaurar

        Transform beforeDeathTransform = FindObjectWithTag("AntesDaMorte");
        if (beforeDeathTransform != null)
        {
            Vector2 targetPosition = beforeDeathTransform.position;
            Debug.Log("Restaurando a posição do jogador para: " + targetPosition);

            if (playerScript.GetComponent<Rigidbody2D>() != null)
            {
                // Se o personagem tem um Rigidbody2D, usa MovePosition
                playerScript.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
            }
            else
            {
                playerScript.transform.position = targetPosition;
            }
            isSaved = false;
            ResumeTime();
        }
        else
        {
            Debug.LogWarning("Objeto com a tag 'AntesDaMorte' não encontrado!");
        }
    }

    private Transform FindObjectWithTag(string tag)
    {
        // Encontra o primeiro objeto com a tag especificada
        GameObject obj = GameObject.FindGameObjectWithTag(tag);
        return obj != null ? obj.transform : null;
    }

    private void PauseTime()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    private void ResumeTime()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    void Reset()
    {
        Time.timeScale = 1;
        isPaused = false;
        isSaved = false;
    }

    private void OnApplicationQuit()
    {
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AntesDaMorte") && playerScript.isDead)
        {
            playerScript.isDead = false; // Permite o movimento novamente
            RestorePosition(); // Restaura a posição após 3 segundos
        }
    }
}
