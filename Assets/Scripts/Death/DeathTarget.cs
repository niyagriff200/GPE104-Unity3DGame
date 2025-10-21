
using UnityEngine;

public class DeathTarget : DeathDestroy
{
    private bool hasDied = false; // Prevents multiple death triggers from overlapping damage or collisions

    public override void Die()
    {
        if (!hasDied)
        {
            hasDied = true;


            // Remove from activeEnemies list—used for tracking win conditions and pacing
            GameManager.instance.RemoveEnemy(gameObject);

            // Call base death logic to destroy the object
            base.Die();
        }
    }
}
