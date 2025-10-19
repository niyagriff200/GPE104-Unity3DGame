using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPoint;

    protected GameObject projectilePrefab;
    protected float fireRate;
    protected float nextFireTime;

    protected virtual void Start()
    {
        nextFireTime = 0f;
    }

    public virtual void Shoot()
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        if (projectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogWarning(name + " is missing a projectile prefab or spawn point.");
            return;
        }

        Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // CORRECTED LOGIC: Cooldown is 1 divided by the rate (shots per second).
        if (fireRate > 0)
        {
            nextFireTime = Time.time + (1f / fireRate);
        }
    }
}