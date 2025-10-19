using UnityEngine;

public class HealthPackPickup : MonoBehaviour
{
    private float healAmount;
    private void Start()
    {
        LevelData levelData = FindFirstObjectByType<LevelData>();
        if (levelData != null)
        {
            healAmount = levelData.healAmount;
        }
        else
        {
            Debug.LogWarning("No LevelData found in the scene!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }
    }

}
