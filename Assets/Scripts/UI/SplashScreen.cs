
using UnityEngine;

// Waits for any key press to transition from splash to main menu
public class SplashScreenUI : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown == true)
        {
            GameManager.instance.ShowMainMenu();
        }
    }
}
