using UnityEngine;

[RequireComponent(typeof(UFOShooter))]
public class UFOPawn : Pawn
{
    private float moveSpeed;
    private float turnSpeed;

    protected override void Start()
    {
        base.Start();

        shooter = GetComponent<UFOShooter>();
        moveSpeed = GameManager.instance.ufoMoveSpeed;
        turnSpeed = GameManager.instance.ufoTurnSpeed;
    }

    public override void Move(Vector3 direction, bool isForce)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public override void Rotate(Vector3 rotationAngles, bool isForce)
    {
        if (rotationAngles == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(rotationAngles);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    public override void Shoot()
    {
        shooter.Shoot();
    }
}
