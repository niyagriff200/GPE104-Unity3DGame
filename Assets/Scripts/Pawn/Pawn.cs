using UnityEngine;

// Base Pawn class used by all entities that can move or rotate in the world.
// This ensures every Pawn has a Rigidbody and defines required movement methods.

public abstract class Pawn : MonoBehaviour
{
    protected Rigidbody rb;

    public abstract void Move(Vector3 moveVector, bool isForce);
    public abstract void Rotate(Vector3 rotationAngles, bool isForce);

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

}
