using UnityEngine;


public class MeteorOnCollisionResponse : OnCollisionResponse
{


    protected override void Start()
    {
        damageAmount = GameManager.instance.meteorDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other); // Use base damage logic
    }

    protected override void HandleEffects(GameObject other)
    {

        // Play rock impact sound if assigned
        if (GameManager.instance.damageRockSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageRockSound, transform.position, 1f);
        }

        KnockbackOnDamage knockback = other.GetComponent<KnockbackOnDamage>();
        if (knockback != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            knockback.ApplyKnockback(hitDirection);
        }
    }
}