
using UnityEngine;

// Handles return to main menu from credits screen
public class CreditsUI : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.instance?.ShowMainMenu();
    }
}
