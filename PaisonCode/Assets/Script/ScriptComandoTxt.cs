using UnityEngine;
using TMPro;

public class GerenciadorInputCodigo : MonoBehaviour
{
    public TMP_InputField inputCodigo; // Agora usa TMP_InputField para TextMeshPro
    public GameObject porta; // Refer�ncia � porta que deve desaparecer
    public string codigoCorreto = "1234"; // C�digo que faz a porta desaparecer

    void Start()
    {
        // Adiciona um listener para o evento de fim de edi��o do TMP_InputField
        inputCodigo.onEndEdit.AddListener(VerificarCodigo);
    }

    // M�todo chamado quando o usu�rio pressiona Enter ou conclui a edi��o no TMP_InputField
    void VerificarCodigo(string codigoDigitado)
    {
        // Verifica se o c�digo digitado � o c�digo correto
        if (codigoDigitado == codigoCorreto)
        {
            // Desativa a porta
            if (porta != null)
            {
                porta.SetActive(false);
            }
        }
    }
}