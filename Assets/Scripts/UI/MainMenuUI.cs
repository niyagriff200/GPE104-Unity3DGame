
using UnityEngine;

// Handles button logic for main menu navigation
public class MainMenuUI : MonoBehaviour
{
    public void StartButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowGameplay();
        }
    }

    public void SettingsButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowSettingsScreen();
        }
    }

    public void CreditsButton()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ShowCreditsScreen();
        }
    }

    public void ExitButton()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("You exited the game.");
            GameManager.instance.QuitGame();
        }
    }
}
