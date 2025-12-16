using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField]
    [Tooltip("The Pipe prefab to instantiate")] 
    private GameObject pipePrefab;
    
    [SerializeField]
    [Tooltip("Time between each pipe spawn")] 
    private float spawnInterval = 2f;
    
    [SerializeField]
    [Tooltip("Minimum height for pipe gap")]
    private float minHeight = -2f;
    
    [SerializeField]
    [Tooltip("Maximum height for pipe gap")]
    private float maxHeight = 2f;

    private float spawnTimer;
    private bool canSpawn = true;

    void Update()
    {
        if (!canSpawn) return;

        spawnTimer += Time.deltaTime;

        // Spawn new pipe when timer reaches interval
        if (spawnTimer >= spawnInterval)
        {
            SpawnPipe();
            spawnTimer = 0f;
        }
    }

    /// <summary>
    /// Spawns a new pipe at a random height in front of the player
    /// </summary>
    void SpawnPipe()
    {
        float randomHeight = Random.Range(minHeight, maxHeight);
        Vector3 spawnPosition = new Vector3(0f, randomHeight, 15f);
        Instantiate(pipePrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Stops spawning pipes (called on game over)
    /// </summary>
    public void StopSpawning()
    {
        canSpawn = false;
    }
}
