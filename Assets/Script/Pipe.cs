using UnityEngine;

public class Pipe : MonoBehaviour
{
    [Header("Pipe Settings")]
    [SerializeField] private float moveSpeed = 3f;

    void Update()
    {
        // Move pipe toward the player (along Z axis)
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;

        // Destroy pipe when it goes behind camera
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}
