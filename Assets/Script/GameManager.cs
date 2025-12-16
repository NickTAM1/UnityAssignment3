using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField]
    [Tooltip("Reference to the TextMeshPro UI component that displays the player's current score.")]
    private TextMeshProUGUI ScoreText;
    [SerializeField]
    [Tooltip("The UI Panel that contains the Game Over text and buttons. This is hidden by default.")] 
    private GameObject gameOverPanel;

    [Header("References")]
    [SerializeField]
    [Tooltip("Reference to the PipeSpawner script. Used to stop pipes from spawning when the game ends.")] 
    private PipeSpawner pipeSpawner;

    // Current score
    private int score = 0;

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
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateScoreText();
    }

    /// <summary>
    /// Adds one point to the score
    /// </summary>
    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    /// <summary>
    /// Updates the score display
    /// </summary>
    void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + score;
        }
    }

    /// <summary>
    /// Called when player dies - shows game over screen
    /// </summary>
    public void GameOver()
    {
        // Stop spawning pipes
        if (pipeSpawner != null)
        {
            pipeSpawner.StopSpawning();
        }

        // Show game over panel
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
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
    /// Returns to main menu
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
