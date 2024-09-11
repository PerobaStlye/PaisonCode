using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject particlePrefab;

    private void Start()
    {
        GameManager.Instance.RegisterParticleListener(CriarParticulas);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterParticleListener(CriarParticulas);
    }

    private void CriarParticulas(Vector2 position, GameObject prefab)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
