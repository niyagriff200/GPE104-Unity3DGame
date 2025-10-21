using UnityEngine;

// Applies movement based on direction from DriftDirection
[RequireComponent(typeof(Rigidbody))]
public class DirectionMover : MonoBehaviour
{
    private float moveSpeed;
    private DriftDirection drift;
    private Rigidbody rb;

    private void Start()
    {
        moveSpeed = GameManager.instance.meteorMoveSpeed;
        drift = GetComponent<DriftDirection>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (drift != null && rb != null)
        {
            Vector3 moveVector = drift.GetDirection() * moveSpeed;
            rb.linearVelocity = moveVector;
        }
    }
}
