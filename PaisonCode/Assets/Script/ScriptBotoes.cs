using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArcadeButton : MonoBehaviour
{
    public string cenaParaIniciar; // Nome da cena que você quer iniciar

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
        Debug.Log("Botão Arcade clicado!");

        // Carrega a cena especificada
        SceneManager.LoadScene(cenaParaIniciar);
    }
}