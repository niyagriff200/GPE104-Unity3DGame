using UnityEngine;

public class UFOPawn : Pawn
{
    private float moveSpeed;
    private float turnSpeed;
    protected override void Start()
    {
        moveSpeed = GameManager.instance.ufoMoveSpeed;
        turnSpeed = GameManager.instance.ufoTurnSpeed;

    }
    public override void Move(Vector3 direction, bool isForce)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    public override void Rotate(Vector3 rotationAngles, bool isForce)
    {
        
    }
    
}
