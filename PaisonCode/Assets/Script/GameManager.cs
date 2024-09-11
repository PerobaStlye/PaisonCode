using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Referências e variáveis gerais
    public GameObject player; // Referência ao objeto player
    public Vector2 personagemPosicao;
    public int moedasColetadas;
    public int outrosItens;
    public bool jogadorEstaSubindo;

    // Eventos para o padrão Observer
    public static event Action<Vector2, GameObject> OnTriggerParticles; // Evento para partículas
    public static event Action<string> OnScreenChange; // Evento para mudança de tela
    public static event Action<string> OnScreenDeactivate; // Evento para desativar tela
    public static event Action<AudioClip> OnPlaySound; // Evento para reprodução de som
    public static event Action<Vector2> OnPlayerPositionChanged; // Evento para ver a posição do jogador

    // ================== INICIALIZAÇÃO ==================
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ================== SALVAR E CARREGAR DADOS DO JOGADOR ==================

    public void SavePlayerData()
    {
        PlayerPrefs.SetFloat("PosicaoX", player.transform.position.x);
        PlayerPrefs.SetFloat("PosicaoY", player.transform.position.y);
        PlayerPrefs.SetInt("Moedas", moedasColetadas);
        PlayerPrefs.SetInt("OutrosItens", outrosItens);
        PlayerPrefs.SetInt("Subindo", jogadorEstaSubindo ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        float x = PlayerPrefs.GetFloat("PosicaoX", 0);
        float y = PlayerPrefs.GetFloat("PosicaoY", 0);
        moedasColetadas = PlayerPrefs.GetInt("Moedas", 0);
        outrosItens = PlayerPrefs.GetInt("OutrosItens", 0);
        jogadorEstaSubindo = PlayerPrefs.GetInt("Subindo", 0) == 1;

        player.transform.position = new Vector2(x, y);
    }

    public void UpdatePlayerPosition(Vector2 newPosition)
    {
        OnPlayerPositionChanged?.Invoke(newPosition); // Dispara o evento
    }

    // ================== MANIPULAÇÃO DE PARTÍCULAS ==================

    public void RegisterParticleListener(Action<Vector2, GameObject> listener)
    {
        OnTriggerParticles += listener;
    }

    public void UnregisterParticleListener(Action<Vector2, GameObject> listener)
    {
        OnTriggerParticles -= listener;
    }

    public void TriggerParticles(Vector2 position, GameObject particlePrefab)
    {
        OnTriggerParticles?.Invoke(position, particlePrefab);
    }

    // ================== MUDANÇA DE TELA ==================

    public void RegisterScreenChangeListener(Action<string> listener)
    {
        OnScreenChange += listener;
    }

    public void UnregisterScreenChangeListener(Action<string> listener)
    {
        OnScreenChange -= listener;
    }

    public void ChangeScreen(string screenName)
    {
        OnScreenChange?.Invoke(screenName);
    }

    public void RegisterScreenDeactivateListener(Action<string> listener)
    {
        OnScreenDeactivate += listener;
    }

    public void UnregisterScreenDeactivateListener(Action<string> listener)
    {
        OnScreenDeactivate -= listener;
    }

    public void DeactivateScreen(string screenName)
    {
        OnScreenDeactivate?.Invoke(screenName);
    }

    // ================== MANIPULAÇÃO DE SONS ==================

    public void RegisterSoundListener(Action<AudioClip> listener)
    {
        OnPlaySound += listener;
    }

    public void UnregisterSoundListener(Action<AudioClip> listener)
    {
        OnPlaySound -= listener;
    }

    public void PlaySound(AudioClip clip)
    {
        OnPlaySound?.Invoke(clip);
    }

    // ================== GERENCIAMENTO GERAL ==================

    // Aqui você pode adicionar funções de gerenciamento geral como, por exemplo, resetar o jogo,
    // pausar o jogo ou gerenciar desafios futuros que forem surgindo.
}
