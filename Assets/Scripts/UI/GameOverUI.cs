
using UnityEngine;
using TMPro;

// Displays score, high score, and win/loss result on game over screen
public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI resultsText;

    private void Update()
    {
        if (GameManager.instance != null)
        {
            scoreText.text = "Score: " + GameManager.instance.score.ToString("0000");
            highScoreText.text = "High Score: " + GameManager.instance.topScore.ToString("0000");
        }
    }

    public void PlayAgain()
    {
        //Checks if instance of GameManager is true before showing gameplay
        GameManager.instance?.ShowGameplay();
    }

    public void MainMenu()
    {
        //Checks if instance of GameManager is true before showing main menu
        GameManager.instance?.ShowMainMenu();
    }

    public void ShowResults()
    {
        bool playerWon = false;

        // Win: all astronauts collected and enough were spawned
        if (GameManager.instance.currentLevelData.activeAstronauts.Count == 0 &&
            GameManager.instance.currentLevelData.initialAstronautsSpawned >= GameManager.instance.currentLevelData.astronautCount)
        {
            playerWon = true;
        }

        // Loss: player pawn is missing
        if (GameManager.instance.currentLevelData.players.Count > 0 &&
            GameManager.instance.currentLevelData.players[0].pawn == null)
        {
            playerWon = false;
        }

        // Update result text
        //if player won is true result text = You Win! else, You Lose!
        resultsText.text = playerWon ? "You Win!" : "You Lose!";
    }
}
