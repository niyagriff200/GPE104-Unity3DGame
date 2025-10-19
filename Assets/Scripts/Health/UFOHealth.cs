
using UnityEngine;

public class UFOHealth : Health
{
    protected override void Start()
    {
        // Pull max health from GameManager—designer-controlled
        maxHealth = GameManager.instance.ufoMaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Die()
    {
        // Award score for UFO kill
        //GameManager.instance.AddScore(GameManager.instance.ufoScore);

        // Remove from activeEnemies and destroy—UFOs use DeathTarget
        DeathTarget target = GetComponent<DeathTarget>();
        if (target != null)
       {
           
            target.Die(); // Handles removal and destruction
        }
    }
}
