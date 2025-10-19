
using UnityEngine;

public class PlayerHealth : Health
{
    // Tracks remaining lives for the player
    private int startingLives;
    private int currentLives;

    protected override void Start()
    {
        // Pull health and lives values from GameManager for designer control
        maxHealth = GameManager.instance.playerMaxHealth;
        currentHealth = maxHealth;
        startingLives = GameManager.instance.playerStartingLives;
        currentLives = startingLives;
    }

    // Returns current number of lives (used by UI and win/lose logic)
    public int GetCurrentLives()
    {
        return currentLives;
    }

    protected override void Die()
    {
        currentLives--;
      

        // If player still has lives, reset health and reposition
        if (currentLives > 0)
        {
            HealToFull(); // Restore health
            DeathRecenter recenter = GetComponent<DeathRecenter>();
            if (recenter != null)
            {
                recenter.Die(); // Reset position instead of destroying
            }

            currentHealth = maxHealth; // Ensure health is restored
        }
        else
        {
            // Final death—trigger visual effect and destroy
            DeathSpin spin = GetComponent<DeathSpin>();
            if (spin != null)
            {
                spin.Die();
            }
        }
    }
}
