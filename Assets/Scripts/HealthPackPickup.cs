using UnityEngine;

public class HealthPackPickup : MonoBehaviour
{
    private float healAmount;
    private void Start()
    {
        healAmount = GameManager.instance.healAmount;
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
