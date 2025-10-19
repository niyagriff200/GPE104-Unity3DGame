
using UnityEngine;

// Handles return to settings screen from controls screen
public class ControlsUI : MonoBehaviour
{
    public void Settings()
    {
        GameManager.instance?.ShowSettingsScreen();
    }
}
