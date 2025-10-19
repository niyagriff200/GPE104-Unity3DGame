using UnityEngine;

public class ProjectileOnCollisionResponse : OnCollisionResponse
{
    protected override void Start()
    {
        damageAmount = GameManager.instance.projectileDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other);
    }

    protected override void HandleCleanup()
    {
        base.HandleCleanup();
    }
}
