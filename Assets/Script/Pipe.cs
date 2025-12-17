using UnityEngine;

/// <summary>
/// Controls individual pipe movement and destruction in 3D space
/// </summary>
public class Pipe : MonoBehaviour
{
    [Header("Pipe Settings")]
    [SerializeField]
    [Tooltip("Starting speed of the pipe")]
    private float _baseSpeed = 3f;

    [SerializeField]
    [Tooltip("How much speed increases per score point")]
    private float _speedIncreasePerScore = 0.6f;

    private float _currentSpeed;


    void Start()
    {
        // Calculate speed based on current score
        _currentSpeed = _baseSpeed + (GameManager.Instance.GetScore() * _speedIncreasePerScore);
    }


    void Update()
    {
        // Move pipe toward the player (along Z axis)
        transform.position += Vector3.back * _currentSpeed * Time.deltaTime;

        // Destroy pipe when it goes behind camera
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}