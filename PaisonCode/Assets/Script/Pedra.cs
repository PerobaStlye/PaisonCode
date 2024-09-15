using UnityEngine;

public class Pedra : MonoBehaviour
{
    public float velocidadeQueda = 2.0f; // Velocidade com que a pedra cai
    public float tempoPedra;

    private void Start()
    {
        // Se necessário, inicialize a pedra aqui
    }

    private void Update()
    {
        tempoPedra = tempoPedra + Time.deltaTime;
        // Faz a pedra cair
        transform.Translate(Vector2.down * velocidadeQueda * Time.deltaTime);

        // Verifica se a pedra saiu da tela e a destrói se necessário
        if (tempoPedra > 6)
        {
            tempoPedra = 0;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Adicione lógica de colisão se necessário
        // Exemplo: se a pedra colidir com o jogador, causar dano, etc.
    }
}
