
using UnityEngine;

public class Health : MonoBehaviour
{
    // Health values used by all damageable entities
    protected float maxHealth;
    protected float currentHealth;

    // Initialize health—subclasses will override maxHealth using GameManager
    protected virtual void Start()
    {
        maxHealth = 100f; // fallback value if not overridden
        currentHealth = maxHealth;
    }

    // Apply damage and check for death
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0f); // clamp to zero

        if (currentHealth == 0f)
        {
            Die(); // trigger death logic
        }
    }

    // Heal by a specific amount
    public virtual void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // clamp to max
    }

    // Fully restore health
    public virtual void HealToFull()
    {
        currentHealth = maxHealth;
    }

    // Instantly kill this entity
    public virtual void InstaKill()
    {
        currentHealth = 0f;
        Die();
    }

    // Check if entity is still alive
    public bool IsAlive()
    {
        return currentHealth > 0f;
    }

    // Return health as a percentage (for UI, DamageFlash, and Sound)
    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    // Default death behavior—can be overridden
    protected virtual void Die()
    {
        Destroy(gameObject); // destroy the entity
    }
}
