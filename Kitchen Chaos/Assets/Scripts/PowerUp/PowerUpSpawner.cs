using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUps Pool")]
    [SerializeField] private GameObject powerUpPrefab; 
    private List<GameObject> pool = new List<GameObject>();
    private int poolSize = 3; // You likely only need 2 or 3 in the scene at once    [SerializeField] private Transform[] spawnPoints;        // Predetermined safe spots
    
    [Header("Spawn Area Constraints")]
    [SerializeField] private Vector2 xRange = new Vector2(-10f, 10f); // x0 and x1
    [SerializeField] private Vector2 zRange = new Vector2(-10f, 10f); // z0 and z1
    [SerializeField] private float spawnHeight = 0.5f;

    [Header("Rules")]
    [SerializeField] private float minDistanceFromPlayer = 5f;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float spawnInterval = 15f;      // Every 15 seconds
    
    private float timer;

    private void Awake() 
    {
        // Pre-warm the pool
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject obj = Instantiate(powerUpPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private void Update() {

        // Only spawn if the game is actually playing
        if (!GameHandler.Instance.IsGamePlaying()) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval) 
        {
            timer = 0;
            SpawnPowerUp();
        }
    }

    private GameObject GetPooledObject() 
    {
        foreach (GameObject obj in pool) 
        {
            if (!obj.activeInHierarchy)
            {
                
            }
            return obj;
        }
        return null; // Pool is full, don't spawn
    }

    private void SpawnPowerUp() {
        GameObject powerUp = GetPooledObject();
        if (powerUp == null) return;

        Vector3 spawnPos = Vector3.zero;
        bool foundValidSpot = false;
        int attempts = 0;

        // Try to find a spot that isn't too close to the player
        while (!foundValidSpot && attempts < 10) {
            float randomX = Random.Range(xRange.x, xRange.y);
            float randomZ = Random.Range(zRange.x, zRange.y);
            spawnPos = new Vector3(randomX, spawnHeight, randomZ);

            if (Vector3.Distance(spawnPos, playerTransform.position) >= minDistanceFromPlayer) {
                foundValidSpot = true;
            }
            attempts++;
        }

        if (foundValidSpot) {
            powerUp.transform.position = spawnPos;
            powerUp.SetActive(true);
        }
    }

    // This makes the "Robust" part easy to see in the Scene View!
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.cyan;
        Vector3 center = new Vector3((xRange.x + xRange.y) / 2, spawnHeight, (zRange.x + zRange.y) / 2);
        Vector3 size = new Vector3(xRange.y - xRange.x, 0.1f, zRange.y - zRange.x);
        Gizmos.DrawWireCube(center, size);
    }
}
