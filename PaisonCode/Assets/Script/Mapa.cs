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
        // Aqui voc� atualiza a representa��o visual do mapa com base na nova posi��o do jogador
        Debug.Log("Mapa atualizado para nova posi��o: " + novaPosicao);
        // Exemplo: mover um �cone no mapa para a nova posi��o
    }
}
