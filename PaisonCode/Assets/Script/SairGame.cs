using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SairGame : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    void Start()
    {
        // Obt�m o componente TextMeshProUGUI associado ao bot�o
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Adiciona um listener para o evento de clique do bot�o
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // M�todo chamado quando o bot�o � clicado
    void OnButtonClick()
    {
        // Adicione a l�gica que deseja executar quando o bot�o � clicado
        Debug.Log("Bot�o de sa�da clicado!");

        // Verifica se est� rodando no Editor e, se n�o, tenta fechar o aplicativo
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}