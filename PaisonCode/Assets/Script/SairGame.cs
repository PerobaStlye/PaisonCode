using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SairGame : MonoBehaviour
{
    private TextMeshProUGUI buttonText;

    void Start()
    {
        // Obtém o componente TextMeshProUGUI associado ao botão
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Adiciona um listener para o evento de clique do botão
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    // Método chamado quando o botão é clicado
    void OnButtonClick()
    {
        // Adicione a lógica que deseja executar quando o botão é clicado
        Debug.Log("Botão de saída clicado!");

        // Verifica se está rodando no Editor e, se não, tenta fechar o aplicativo
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}