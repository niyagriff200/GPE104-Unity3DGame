using UnityEngine;

// Recenter the player
public class DeathRecenter : Death
{
    [SerializeField] private Transform defaultSpawnPoint;

    public override void Die()
    {
        Transform spawnPoint = null;

        // Try using the level's spawn point first
        if (GameManager.instance != null && GameManager.instance.currentLevelData != null)
        {
            spawnPoint = GameManager.instance.currentLevelData.playerSpawnPoint;
        }

        // Fallback to a manually assigned default spawn
        if (spawnPoint == null)
        {
            spawnPoint = defaultSpawnPoint;
        }

        // If no spawn is found, do nothing but log
        if (spawnPoint == null)
        {
            Debug.LogWarning(name + " has no spawn point set.");
            return;
        }

        // Instantly move object to the spawn point
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        // Reset movement if it has a Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
