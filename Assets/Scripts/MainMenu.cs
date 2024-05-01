using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // TODO: Create a State Manager to manage the game states
    public void NewGame()
    {
        SceneManager.LoadScene("Scene-1");
        // TODO: if there are three saved games, prompt the user to overwrite one
    }
    
    public void LoadGame()
    {
        // TODO: load the saved game
    }

    public void Statistics()
    {
        // TODO: Show the player's statistics
        // 1. Shot's accuracy
        // 2. Distance traveled
        // 3. Time played (format: HH:MM:SS)
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