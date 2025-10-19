using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        // Prevents lingering projectiles in the scene
        if (GameManager.instance != null)
        {
            Destroy(gameObject, GameManager.instance.projectileLifetime);
        }
        else
        {
            Destroy(gameObject, 5f); // fallback in case GameManager is missing
        }
    }
}
