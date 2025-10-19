
using UnityEngine;

// Handles navigation from settings screen
public class SettingsUI : MonoBehaviour
{
    public void Controls()
    {
        GameManager.instance?.ShowControlsScreen();
    }

    public void MainMenu()
    {
        GameManager.instance?.ShowMainMenu();
    }
}
