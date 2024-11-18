using UnityEngine;

public class GameOverOnCollision : MonoBehaviour
{
    // Detecta a colis�o com o player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Fecha o jogo
            Application.Quit();

            // Para testar no editor (j� que o Application.Quit n�o funciona no editor)
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
