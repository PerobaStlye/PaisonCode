using UnityEngine;
using TMPro;

public class TextoPlayer : MonoBehaviour
{
    public TMP_InputField inputField; // Referência ao InputField no Unity
    public TextMeshProUGUI textoExibido; // Referência ao TextMeshProUGUI no Unity
    public GameObject indicadorEscrita; // Referência à linha vertical indicadora

    private int contadorAtualizacoes = 0;
    public int limiteAtualizacoes = 3; // Limite de vezes que o texto pode ser atualizado

    void Start()
    {
        // Desativa o indicador de escrita no início
        if (indicadorEscrita != null)
        {
            indicadorEscrita.SetActive(false);
        }
    }

    // Método chamado quando o botão é clicado
    public void AtualizarTexto()
    {
        // Verifica se o limite de atualizações não foi atingido
        if (contadorAtualizacoes < limiteAtualizacoes)
        {
            // Use a variável de classe inputField
            string textoDigitado = inputField.text;

            // Exibe o texto na tela
            textoExibido.text = "Texto digitado: " + textoDigitado;

            // Ativa o indicador de escrita
            if (indicadorEscrita != null)
            {
                indicadorEscrita.SetActive(true);
            }

            // Incrementa o contador de atualizações
            contadorAtualizacoes++;
        }
        else
        {
            // Se o limite for atingido, você pode mostrar uma mensagem ou tomar outra ação
            Debug.Log("Limite de atualizações atingido!");

            // Desativa o indicador de escrita
            if (indicadorEscrita != null)
            {
                indicadorEscrita.SetActive(false);
            }

            // Reinicializa o contador para permitir atualizações adicionais
            contadorAtualizacoes = 0;
        }
    }
}