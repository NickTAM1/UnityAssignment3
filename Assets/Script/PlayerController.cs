using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpForce = 5f;
    [Tooltip("How fast the bird rotates when moving")]
    [SerializeField] private float rotationSpeed = 3f;

    private Rigidbody rb;
    private bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Only allow input if player is alive
        if (!isAlive) return;

        // Jump when spacebar or mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // Rotate bird based on velocity
        RotateBird();
    }

    /// <summary>
    /// Makes the bird jump upward
    /// </summary>
    void Jump()
    {
        rb.linearVelocity = new Vector3(0, jumpForce, 0);
    }

    /// <summary>
    /// Rotates the bird based on its vertical movement
    /// </summary>
    void RotateBird()
    {
        float rotation = rb.linearVelocity.y * rotationSpeed;
        rotation = Mathf.Clamp(rotation, -90f, 45f);
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }

    /// <summary>
    /// Handles collision with pipes or ground
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        // Game over when hitting anything
        isAlive = false;
        GameManager.Instance.GameOver();
    }

    /// <summary>
    /// Triggers when passing through pipe gap
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        // Add score when passing through pipe
        if (other.CompareTag("ScoreZone"))
        {
            GameManager.Instance.AddScore();
        }
    }
}
