using UnityEngine;

public class Mapa : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnPlayerPositionChanged += AtualizarMapa;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerPositionChanged -= AtualizarMapa;
    }

    private void AtualizarMapa(Vector2 novaPosicao)
    {
        // Aqui você atualiza a representação visual do mapa com base na nova posição do jogador
        Debug.Log("Mapa atualizado para nova posição: " + novaPosicao);
        // Exemplo: mover um ícone no mapa para a nova posição
    }
}
