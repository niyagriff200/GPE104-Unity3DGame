
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
        GameManager.instance?.ShowGameplay();
    }

    public void MainMenu()
    {
        GameManager.instance?.ShowMainMenu();
    }

    public void ShowResults()
    {
        bool playerWon = false;

        // Win: all enemies defeated and enough were spawned
        if (GameManager.instance.currentLevelData.activeEnemies.Count == 0 &&
            GameManager.instance.currentLevelData.initialEnemiesSpawned >= GameManager.instance.currentLevelData.enemyCount)
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
        resultsText.text = playerWon ? "You Win!" : "You Lose!";
    }
}
