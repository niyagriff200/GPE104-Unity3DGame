using UnityEngine;

public class PlayerController : Controller
{
    public PlayerPawn pawn;

    private void Update()
    {
        if (pawn != null)
        {

            // Movement: W = forward, S = backward
            Vector3 moveVector = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                moveVector += pawn.transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveVector -= pawn.transform.forward;
            }
            pawn.Move(moveVector, false);

            // Rotation: A/D = yaw, Q/E = roll, Z/X = pitch
            Vector3 rotationVector = Vector3.zero;

            // Yaw (A/S)
            if (Input.GetKey(KeyCode.A))
            {
                rotationVector.y -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rotationVector.y += 1;
            }

            // Roll (Q/E)
            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.z += 1;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.z -= 1;
            }

            // Pitch (Z/X)
            if (Input.GetKey(KeyCode.Z))
            {
                rotationVector.x += 1f;
            }
            if (Input.GetKey(KeyCode.X))
            {
                rotationVector.x -= 1f;
            }

            pawn.Rotate(rotationVector, false);

            // Shoot (Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pawn.Shoot();
            }
        }
    }
}
