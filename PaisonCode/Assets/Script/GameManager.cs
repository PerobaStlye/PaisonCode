using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject player; // Referência ao objeto player
    public Vector2 personagemPosicao;
    public int moedasColetadas;
    public int outrosItens;
    public bool jogadorEstaSubindo;

    public static Action<Vector2> OnTriggerParticles;
    public static Action<string> OnScreenChange;
    public static Action<AudioClip> OnPlaySound;

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

    public void TriggerParticles(Vector2 position)
    {
        OnTriggerParticles?.Invoke(position);
    }

    public void ChangeScreen(string screenName)
    {
        OnScreenChange?.Invoke(screenName);
    }

    public void PlaySound(AudioClip clip)
    {
        OnPlaySound?.Invoke(clip);
    }
}
