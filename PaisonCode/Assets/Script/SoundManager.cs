using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    public AudioClip clipEmpurrar, clipMoeda, clipMorte;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        // Registra o SoundManager no GameManager para ouvir eventos de som
        GameManager.Instance.RegisterSoundListener(ReproduzirSom);
    }

    private void OnDestroy()
    {
        // Remove o SoundManager do evento ao ser destruído
        GameManager.Instance.UnregisterSoundListener(ReproduzirSom);
    }

    private void ReproduzirSom(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            // Reproduz o som usando o AudioSource
            audioSource.PlayOneShot(clip);
        }
    }

    public void TocarSomDeEmpurrar()
    {
        Debug.LogError("Tentando reproduzir som");
        GameManager.Instance.PlaySound(clipEmpurrar);
    }

    public void TocarSomDeMoeda()
    {
        GameManager.Instance.PlaySound(clipMoeda);
    }

    public void TocarSomDeMorte()
    {
        GameManager.Instance.PlaySound(clipMorte);
    }
}
