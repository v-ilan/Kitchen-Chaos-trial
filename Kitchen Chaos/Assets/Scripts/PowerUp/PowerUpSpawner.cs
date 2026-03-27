using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUps Pool")]
    [SerializeField] private List<PowerUpSO> powerUpPrototypes; // List containing Speed and Time SOs
    private int numPowerUps = 3; // You likely only need 2 or 3 of each powerUp in the scene at once
    private List<GameObject> pool = new List<GameObject>();
    private int poolSize = 0;
    private int[] indices;
    
    [Header("Spawn Area Constraints")]
    [SerializeField] private Vector2 xRange = new Vector2(-10f, 10f); // x0 and x1
    [SerializeField] private Vector2 zRange = new Vector2(-10f, 10f); // z0 and z1
    [SerializeField] private float spawnHeight = 0.5f;

    [Header("Rules")]
    [SerializeField] private float minDistanceFromPlayer = 5f;
    [SerializeField] private float spawnInterval = 15f;      // Every 15 seconds
    
    private float timer;

    private void Awake() 
    {
        // Pre-warm the pool
        foreach(var powerUpPrototype in powerUpPrototypes)
        {
            for (int i = 0; i < numPowerUps; i++) 
            {
                GameObject obj = Instantiate(powerUpPrototype.Prefab.gameObject, transform);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
        poolSize = pool.Count;
        // Initialize the indices array
        indices = new int[poolSize];
        for (int i = 0; i < poolSize; i++) 
        {
            indices[i] = i;
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
        ShuffleIndices();
        // Now check the objects in that shuffled order
        for (int i = 0; i < indices.Length; i++) 
        {
            int poolIndex = indices[i];
            if (!pool[poolIndex].activeInHierarchy)
            {
                return pool[poolIndex];
            }
        }
        return null; // Pool is full, don't spawn
    }

    private void SpawnPowerUp() {
        GameObject powerUp = GetPooledObject();
        if (powerUp == null) return;

        bool foundValidSpot = FindValidSpawnPos(out Vector3 spawnPos);
        
        if (foundValidSpot) 
        {
            powerUp.transform.position = spawnPos;
            powerUp.SetActive(true);
        }
    }

    private bool FindValidSpawnPos(out Vector3 potentialPos )
    {
        Vector3 spawnPos = Vector3.zero;
        bool foundValidSpot = false;
        int attempts = 0;

        // Try to find a spot that isn't too close to the player
        while (!foundValidSpot && attempts < 10) {
            float randomX = Random.Range(xRange.x, xRange.y);
            float randomZ = Random.Range(zRange.x, zRange.y);
            spawnPos = new Vector3(randomX, spawnHeight, randomZ);

            if (IsSpawnPosValid(spawnPos)) 
            {
                foundValidSpot = true;
            }
            attempts++;
        }
        potentialPos = spawnPos;
        return foundValidSpot;
    }

    private bool IsSpawnPosValid(Vector3 potentialPos) 
    {  
        // 1. Check Distance from Player
        if (Vector3.Distance(potentialPos, PlayerController.Instance.transform.position) < minDistanceFromPlayer) 
        {
            return false;
        }

        // 2. Check for Overlap with Counters/Walls
        // Radius should be slightly larger than the powerup (e.g., 0.7f)
        /*
        if (Physics.CheckSphere(potentialPos, 0.7f, counterLayerMask)) 
        {
            return false;
        }
        */

        // 3. Check for Overlap with other ACTIVE Power-Ups
        foreach (GameObject activeObj in pool) 
        {
            if (activeObj.activeInHierarchy) 
            {
                // If the potential spot is too close to an existing power-up
                if (Vector3.Distance(potentialPos, activeObj.transform.position) < 2.0f) 
                {
                    return false;
                }
            }
        }

        return true; // All checks passed
    }

    private void ShuffleIndices()
    {
        int randomIndex = 0;
        int temp = 0;
        // Shuffle the indices array using Fisher-Yates
        for (int i = indices.Length - 1; i > 0; i--) 
        {
            randomIndex = Random.Range(0, i + 1);
            temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
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
