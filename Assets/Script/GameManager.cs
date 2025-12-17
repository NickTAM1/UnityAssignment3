using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField]
    [Tooltip("Text element that displays the current score")]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    [Tooltip("Panel that shows when game is over")]
    private GameObject _gameOverPanel;

    [Header("References")]
    [SerializeField]
    [Tooltip("Reference to the pipe spawner to stop spawning on game over")]
    private PipeSpawner _pipeSpawner;

    // Current score
    private int _score = 0;

    /// <summary>
    /// Sets up singleton instance
    /// </summary>
    void Awake()
    {
        // Setup singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   
    void Start()
    {
        // Hide game over panel at start
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }

        UpdateScoreText();
    }

    /// <summary>
    /// Adds one point to the score and updates display
    /// </summary>
    public void AddScore()
    {
        _score++;
        UpdateScoreText();
    }

    /// <summary>
    /// Gets the current score value (used for speed increase)
    /// </summary>
    public int GetScore()
    {
        return _score;
    }

    /// <summary>
    /// Updates the score text display
    /// </summary>
    void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _score;
        }
    }

    /// <summary>
    /// Called when player dies - shows game over screen and stops game
    /// </summary>
    public void GameOver()
    {
        // Stop spawning pipes
        if (_pipeSpawner != null)
        {
            _pipeSpawner.StopSpawning();
        }

        // Show game over panel
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }

        // Stop time
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Restarts the current game scene
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Returns to main menu scene
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}