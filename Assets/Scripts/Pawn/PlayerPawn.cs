using UnityEngine;

[RequireComponent(typeof(PlayerShooter))]
public class PlayerPawn : Pawn
{
    private float moveSpeed;
    private float turnSpeed;
    private float fireCooldown;
    private float fireRate;

    protected override void Start()
    {
        base.Start();

        shooter = GetComponent<PlayerShooter>();
        moveSpeed = GameManager.instance.playerMoveSpeed;
        turnSpeed = GameManager.instance.playerTurnSpeed;
        fireRate = GameManager.instance.playerFireRate;
    }

    public override void Move(Vector3 moveVector, bool isForce)
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public override void Rotate(Vector3 rotationAngles, bool isForce)
    {
        transform.Rotate(rotationAngles * turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        shooter.Shoot();
    }
}
