using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerPawn : Pawn
{
    private float moveForce;
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

        moveForce = GameManager.instance.playerMoveForce;
        turnSpeed = GameManager.instance.playerTurnSpeed;

    }

    public override void Move(Vector3 moveVector, bool isForce)
    {
        if (isForce)
        {
            rb.AddForce(moveVector * moveForce);
        }
        else
        {
            transform.position += moveVector * moveForce * Time.deltaTime;
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
            transform.Rotate(rotationAngles *  turnSpeed * Time.deltaTime);
        }
    }

    // NOTE: Set Rigidbody drag and angular drag in the Inspector
    // to make the ship naturally slow down without code.
}
