using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    [Tooltip("How strong the upward jump force is")]
    private float _jumpForce = 5f;

    [SerializeField]
    [Tooltip("How fast the bird rotates when moving")]
    private float _rotationSpeed = 3f;

    [Header("Visual Effects")]
    [SerializeField]
    [Tooltip("Color to flash when scoring a point")]
    private Color _scoreFlashColor = Color.yellow;

    [SerializeField]
    [Tooltip("How long the flash effect lasts in seconds")]
    private float _flashDuration = 0.2f;

    private Rigidbody _rigidbody;
    private bool _isAlive = true;
    private Renderer _playerRenderer;
    private Color _originalColor;
    private float _flashTimer = 0f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerRenderer = GetComponent<Renderer>();

        // Save original color
        if (_playerRenderer != null)
        {
            _originalColor = _playerRenderer.material.color;
        }
    }

  
    void Update()
    {
        // Only allow input if player is alive
        if (!_isAlive) return;

        // Jump when spacebar or mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        // Rotate bird based on velocity
        RotateBird();

        // Handle color flash
        HandleFlash();
    }

    /// <summary>
    /// Makes the bird jump upward by applying velocity
    /// </summary>
    void Jump()
    {
        _rigidbody.linearVelocity = new Vector3(0, _jumpForce, 0);
    }

    /// <summary>
    /// Rotates the bird based on its vertical movement speed
    /// </summary>
    void RotateBird()
    {
        float rotation = _rigidbody.linearVelocity.y * _rotationSpeed;
        rotation = Mathf.Clamp(rotation, -90f, 45f);
        transform.rotation = Quaternion.Euler(rotation, 0, 0);
    }

    /// <summary>
    /// Handles the score flash effect timer and color reset
    /// </summary>
    void HandleFlash()
    {
        if (_flashTimer > 0f)
        {
            _flashTimer -= Time.deltaTime;

            if (_flashTimer <= 0f && _playerRenderer != null)
            {
                // Reset to original color
                _playerRenderer.material.color = _originalColor;
            }
        }
    }

    /// <summary>
    /// Triggers color flash effect when player scores
    /// </summary>
    public void FlashColor()
    {
        if (_playerRenderer != null)
        {
            _playerRenderer.material.color = _scoreFlashColor;
            _flashTimer = _flashDuration;
        }
    }

    /// <summary>
    /// Handles collision with pipes or ground - triggers game over
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        // Game over when hitting anything
        _isAlive = false;
        GameManager.Instance.GameOver();
    }

    /// <summary>
    /// Triggers when passing through pipe gap - adds score and flashes
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        // Add score when passing through pipe
        if (other.CompareTag("ScoreZone"))
        {
            GameManager.Instance.AddScore();
            FlashColor();
        }
    }
}