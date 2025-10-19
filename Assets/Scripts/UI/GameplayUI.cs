
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Updates score, health bar, and lives icons during gameplay
public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image healthBar;
    public GameObject lifeIconPrefab;
    public Transform livesContainer;

    private GameObject[] lifeIcons;

    private void Start()
    {
        PlayerHealth health = GameManager.instance.currentLevelData.players[0].pawn.health as PlayerHealth;
        if (health != null)
        {
            InitializeLives(health.GetCurrentLives());
            UpdateLives(health.GetCurrentLives());
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString("0000");

        Health health = GameManager.instance.currentLevelData.players[0].pawn.health;
        healthBar.fillAmount = health.GetHealthPercent();
    }

    public void InitializeLives(int totalLives)
    {
        // Clear existing icons
        foreach (Transform child in livesContainer)
        {
            Destroy(child.gameObject);
        }

        // Spawn icons based on totalLives
        lifeIcons = new GameObject[totalLives];
        for (int i = 0; i < totalLives; i++)
        {
            GameObject icon = Instantiate(lifeIconPrefab, livesContainer);
            lifeIcons[i] = icon;
        }
    }

    public void UpdateLives(int currentLives)
    {
        // Toggle icons based on currentLives
        if (lifeIcons == null || lifeIcons.Length == 0)
        {
            return;
        }

        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < currentLives);
        }
    }
}
