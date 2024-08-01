using UnityEngine;
using TMPro;

public class TextoPlayer : MonoBehaviour
{
    public TMP_InputField inputField; // Refer�ncia ao InputField no Unity
    public TextMeshProUGUI textoExibido; // Refer�ncia ao TextMeshProUGUI no Unity
    public GameObject indicadorEscrita; // Refer�ncia � linha vertical indicadora

    private int contadorAtualizacoes = 0;
    public int limiteAtualizacoes = 3; // Limite de vezes que o texto pode ser atualizado

    void Start()
    {
        // Desativa o indicador de escrita no in�cio
        if (indicadorEscrita != null)
        {
            indicadorEscrita.SetActive(false);
        }
    }

    // M�todo chamado quando o bot�o � clicado
    public void AtualizarTexto()
    {
        // Verifica se o limite de atualiza��es n�o foi atingido
        if (contadorAtualizacoes < limiteAtualizacoes)
        {
            // Use a vari�vel de classe inputField
            string textoDigitado = inputField.text;

            // Exibe o texto na tela
            textoExibido.text = "Texto digitado: " + textoDigitado;

            // Ativa o indicador de escrita
            if (indicadorEscrita != null)
            {
                indicadorEscrita.SetActive(true);
            }

            // Incrementa o contador de atualiza��es
            contadorAtualizacoes++;
        }
        else
        {
            // Se o limite for atingido, voc� pode mostrar uma mensagem ou tomar outra a��o
            Debug.Log("Limite de atualiza��es atingido!");

            // Desativa o indicador de escrita
            if (indicadorEscrita != null)
            {
                indicadorEscrita.SetActive(false);
            }

            // Reinicializa o contador para permitir atualiza��es adicionais
            contadorAtualizacoes = 0;
        }
    }
}