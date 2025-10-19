
using UnityEngine;

// Recenter the player
public class DeathRecenter : Death
{
    public override void Die()
    {
        transform.position = GameManager.instance.currentLevelData.playerSpawnPoint.position; //Reset position
    }
}
