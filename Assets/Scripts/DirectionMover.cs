using UnityEngine;

// Moves an object in a random direction at a set speed
public class DirectionMover : MonoBehaviour
{
    private float moveSpeed;         // Movement speed pulled from GameManager
    private Vector3 moveDirection;   // Direction to move in

    private void Start()
    {
        GetRandomDirection(); // Initialize with a random direction
    }

    private void Update()
    {
        MoveForward(); // Continuously move in that direction
    }

    public void GetRandomDirection()
    {
        // Pull speed from GameManager and assign a random 2D direction
        moveSpeed = GameManager.instance.meteorMoveSpeed;
        moveDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    public void MoveForward()
    {
        // Normalize direction and apply movement based on speed and deltaTime
        Vector3 moveVector = moveDirection.normalized;
        moveVector *= moveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }
}