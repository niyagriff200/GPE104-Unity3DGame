using UnityEngine;

public class UFOShooter : ProjectileShooter
{
    protected override void Start()
    {
        base.Start();

        // Pull UFO projectile data from GameManager for centralized tuning
        projectilePrefab = GameManager.instance.ufoProjectilePrefab;
        fireRate = GameManager.instance.ufoFireRate;
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}
