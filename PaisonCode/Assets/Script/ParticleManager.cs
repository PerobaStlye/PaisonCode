using UnityEngine;
using System;
using System.Collections;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    public GameObject rockPrefab; // Prefab da pedra
    public GameObject spawnPoint; // Ponto de spawn das pedras
    private float rockSpawnInterval;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Registra o método para lidar com o intervalo de spawn de pedras
        GameManager.OnRockSpawnIntervalChanged += UpdateSpawnInterval;
        StartCoroutine(SpawnRocks());
    }

    private void OnDestroy()
    {
        // Desregistra o método ao destruir o objeto
        GameManager.OnRockSpawnIntervalChanged -= UpdateSpawnInterval;
    }

    private void UpdateSpawnInterval(float interval)
    {
        rockSpawnInterval = interval;
    }

    private IEnumerator SpawnRocks()
    {
        while (true)
        {
            yield return new WaitForSeconds(rockSpawnInterval);
            SpawnRock();
        }
    }

    private void SpawnRock()
    {
        if (rockPrefab != null && spawnPoint != null)
        {
            // Spawna a pedra na posição centralizada do ponto de spawn
            Vector2 spawnPosition = spawnPoint.transform.position;
            Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
