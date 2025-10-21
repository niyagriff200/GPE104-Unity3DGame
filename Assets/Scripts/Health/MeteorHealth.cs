using UnityEngine;

public class MeteorHealth : Health
{
    protected override void Start()
    {
        // Pull max health from GameManager—designer-controlled
        maxHealth = GameManager.instance.meteorMaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Die()
    {
        // Award score based on size
        GameManager.instance.AddScore(GameManager.instance.meteorScore);

        // Remove from activeEnemies and destroy—meteors use DeathTarget
        DeathTarget target = GetComponent<DeathTarget>();
        if (target != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.deathSound, transform.position, 1f);
            target.Die(); // Handles removal and destruction
        }
    }

}