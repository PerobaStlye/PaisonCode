using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player;
    public Vector2 personagemPosicao;
    public int moedasColetadas;
    public int outrosItens;
    public bool jogadorEstaSubindo;

    public static event Action<Vector2, GameObject> OnTriggerParticles;
    public static event Action<string> OnScreenChange;
    public static event Action<string> OnScreenDeactivate;
    public static event Action<AudioClip> OnPlaySound;
    public static event Action<Vector2> OnPlayerPositionChanged;
    public static event Action<int> OnMoedaColetada;
    public static event Action<float> OnRockSpawnIntervalChanged; // Evento para o intervalo das pedras

    public float intervaloPedras = 5f; // Intervalo padrão para spawn de pedras

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

    private void Start()
    {
        // Define o intervalo inicial de spawn das pedras
        OnRockSpawnIntervalChanged?.Invoke(intervaloPedras);
    }

    public void UpdatePlayerPosition(Vector2 newPosition)
    {
        OnPlayerPositionChanged?.Invoke(newPosition);
    }

    public void AddMoedas(int quantidade)
    {
        moedasColetadas += quantidade;
        OnMoedaColetada?.Invoke(moedasColetadas);
    }

    // Métodos para manipulação de eventos
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
}
