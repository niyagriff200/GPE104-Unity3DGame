using UnityEngine;

public class PlayerShooter : ProjectileShooter
{
    protected override void Start()
    {
        base.Start();

        // Pull player projectile data from GameManager for centralized tuning
        projectilePrefab = GameManager.instance.playerProjectilePrefab;
        fireRate = GameManager.instance.playerFireRate;
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}
