using UnityEngine;

[RequireComponent(typeof(PlayerShooter))]
public class PlayerPawn : Pawn
{
    private float moveSpeed;
    private float turnSpeed;

    protected override void Start()
    {
        base.Start();

        // Get Rigidbody so I can use Unity's physics
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("There is no rigid body on " + gameObject.name);
        }

        moveSpeed = GameManager.instance.playerMoveSpeed;
        turnSpeed = GameManager.instance.playerTurnSpeed;

    }

    public override void Move(Vector3 moveVector, bool isForce)
    {
        if (isForce)
        {
            rb.AddForce(moveVector * moveSpeed);
        }
        else
        {
            transform.position += moveVector * moveSpeed * Time.deltaTime;
        }

    }

    public override void Rotate(Vector3 rotationAngles, bool isForce)
    {
        if (isForce)
        {
            rb.AddRelativeTorque(turnSpeed * rotationAngles);
        }
        else
        {
            transform.Rotate(rotationAngles * turnSpeed * Time.deltaTime);
        }
    }
    public override void Shoot()
    {
        shooter.Shoot();
    }
}
