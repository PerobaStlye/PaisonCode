using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioSource audioSource, sfxSource;
    public AudioClip clipEmpurrar, clipMoeda, clipMorte;


    private void Start()
    {
        GameManager.Instance.RegisterSoundListener(ReproduzirSom);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterSoundListener(ReproduzirSom);
    }

    private void ReproduzirSom(AudioClip clip)
    {
       
    }

}
