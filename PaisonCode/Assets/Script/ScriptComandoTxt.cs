using UnityEngine;
using TMPro;

public class GerenciadorInputCodigo : MonoBehaviour
{
    public TMP_InputField inputCodigo; // Agora usa TMP_InputField para TextMeshPro
    public GameObject porta; // Referência à porta que deve desaparecer
    public string codigoCorreto = "1234"; // Código que faz a porta desaparecer

    void Start()
    {
        // Adiciona um listener para o evento de fim de edição do TMP_InputField
        inputCodigo.onEndEdit.AddListener(VerificarCodigo);
    }

    // Método chamado quando o usuário pressiona Enter ou conclui a edição no TMP_InputField
    void VerificarCodigo(string codigoDigitado)
    {
        // Verifica se o código digitado é o código correto
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