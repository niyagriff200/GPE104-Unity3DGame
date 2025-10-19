using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Health health;
    [HideInInspector] public ProjectileShooter shooter;
    protected Rigidbody rb;

    public abstract void Move(Vector3 moveVector, bool isForce);
    public abstract void Rotate(Vector3 rotationAngles, bool isForce);

    public virtual void Shoot()
    {
        if (shooter != null)
        {
            shooter.Shoot();
        }
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        shooter = GetComponentInChildren<ProjectileShooter>();
        health = GetComponentInChildren<Health>();
    }
}
