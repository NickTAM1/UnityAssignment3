using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Starts the game by loading the Game scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
