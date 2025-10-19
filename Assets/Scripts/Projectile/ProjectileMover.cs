using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private float moveSpeed;

    private void Start()
    {
        // Pull projectile speed from GameManager for centralized tuning
        moveSpeed = GameManager.instance.projectileSpeed;
    }

    private void Update()
    {
        // Moves forward relative to local up direction
        transform.position -= transform.up * moveSpeed * Time.deltaTime;
    }
}
