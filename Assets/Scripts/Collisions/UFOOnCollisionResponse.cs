
using UnityEngine;


public class UFOOnCollisionResponse : OnCollisionResponse
{
   

    protected override void HandleEffects(GameObject other)
    {
        // Play metal impact sound if assigned
        if (GameManager.instance.damageMetalSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageMetalSound, transform.position, 1f);
        }

        // Apply knockback to damaged object if it supports it
        KnockbackOnDamage knockback = other.GetComponent<KnockbackOnDamage>();
        if (knockback != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            knockback.ApplyKnockback(hitDirection);
        }
    }
}