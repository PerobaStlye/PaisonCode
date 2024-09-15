using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] screenPrefabs; // Array de prefabs de tela para ativar/desativar
    private Dictionary<string, GameObject> screenInstances = new Dictionary<string, GameObject>();
    private bool test = true;
    int reiniciarTela;
    int contadorPraTela;
    int streak;

    private void Start()
    {
        GameManager.Instance.RegisterScreenChangeListener(TrocarTela);
        GameManager.Instance.RegisterScreenDeactivateListener(DeactivateScreen);

        // Instanciar as telas e adicioná-las ao dicionário
        foreach (var prefab in screenPrefabs)
        {
            GameObject screen = Instantiate(prefab);
            screen.SetActive(false);
            screenInstances[prefab.name] = screen;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterScreenChangeListener(TrocarTela);
        GameManager.Instance.UnregisterScreenDeactivateListener(DeactivateScreen);
    }

    private void TrocarTela(string screenName)
    {
        // Desativa todas as telas
        foreach (var screenInstance in screenInstances.Values)
        {
            if (screenInstance.activeSelf)
            {
                // Se a tela estiver ativa e for diferente da tela que deve ser ativada
                if (screenInstance.name != screenName)
                {
                    screenInstance.SetActive(false);
                }
            }
        }

        // Ativa a tela correspondente ao nome
        if (screenInstances.TryGetValue(screenName, out GameObject screenToActivate))
        {
            screenToActivate.SetActive(true);
        }
    }

    private void DeactivateScreen(string screenName)
    {
        if (screenInstances.TryGetValue(screenName, out GameObject screenToDeactivate))
        {
            screenToDeactivate.SetActive(false);
        }
    }

    //////////////////////////////////////////////////TESTE///////////////////////////////////////////////////////////
    private void Update()
    {


        if (test == true)
        {
            GameManager.Instance.ChangeScreen($"Puzzle1");
            test = false;
        }
    }
}
