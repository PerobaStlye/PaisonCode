using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab; // Prefab das partículas

    private void OnEnable()
    {
        GameManager.OnTriggerParticles += SpawnParticles;
    }

    private void OnDisable()
    {
        GameManager.OnTriggerParticles -= SpawnParticles;
    }

    private void SpawnParticles(Vector2 position)
    {
        Instantiate(particlePrefab, position, Quaternion.identity);
    }
}
