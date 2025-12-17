using UnityEngine;


public class PipeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField]
    [Tooltip("The pipe prefab to spawn")]
    private GameObject _pipePrefab;

    [SerializeField]
    [Tooltip("Time between each pipe spawn in seconds")]
    private float _spawnInterval = 2f;

    [SerializeField]
    [Tooltip("Minimum height for pipe gap")]
    private float _minHeight = -2f;

    [SerializeField]
    [Tooltip("Maximum height for pipe gap")]
    private float _maxHeight = 2f;

    private float _spawnTimer;
    private bool _canSpawn = true;

    /// <summary>
    /// Updates spawn timer and spawns pipes at intervals
    /// </summary>
    void Update()
    {
        if (!_canSpawn) return;

        _spawnTimer += Time.deltaTime;

        // Spawn new pipe when timer reaches interval
        if (_spawnTimer >= _spawnInterval)
        {
            SpawnPipe();
            _spawnTimer = 0f;
        }
    }

    /// <summary>
    /// Spawns a new pipe at a random height in front of the player
    /// </summary>
    void SpawnPipe()
    {
        float randomHeight = Random.Range(_minHeight, _maxHeight);
        Vector3 spawnPosition = new Vector3(0f, randomHeight, 15f);
        Instantiate(_pipePrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Stops spawning pipes when called (used on game over)
    /// </summary>
    public void StopSpawning()
    {
        _canSpawn = false;
    }
}