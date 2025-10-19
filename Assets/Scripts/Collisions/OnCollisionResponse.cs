using UnityEngine;

public abstract class OnCollisionResponse : MonoBehaviour
{
    protected float damageAmount = 25f;
    protected bool instantKill = false;


    protected virtual void Start()
    {

    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer)
        {
            return;
        }

        HandleDamage(other.gameObject);
        HandleEffects(other.gameObject);
        HandleCleanup();
    }

    protected virtual void HandleDamage(GameObject other)
    {
        Health health = other.GetComponentInChildren<Health>();
        if (health != null)
        {
            if (instantKill)
            {
                health.InstaKill();
            }
            else
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    protected virtual void HandleEffects(GameObject other)
    {
        // Can be used later for visual feedback or camera shake
    }

    protected virtual void HandleCleanup()
    {
        // Cleans up the projectile after collision
        if (GetComponent<Projectile>() != null)
        {
            Destroy(gameObject);
        }
    }
}
