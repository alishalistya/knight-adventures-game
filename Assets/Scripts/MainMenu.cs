using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.NewGame();
    }

    public void Settings()
    {
        // TODO: Show the player's settings
        // 1. Player's name
        // 2. Sound volume
        // 3. Music volume
        // 4. Difficulty level
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}