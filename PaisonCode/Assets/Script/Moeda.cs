using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valorMoeda = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adiciona moedas ao GameManager
            GameManager.Instance.AddMoedas(valorMoeda);

            // Destroi a moeda após ser coletada
            Destroy(gameObject);
        }
    }
}
