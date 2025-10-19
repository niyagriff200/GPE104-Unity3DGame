using UnityEngine;

public class UFOController : Controller
{
    [HideInInspector] public Pawn pawn;
    [HideInInspector] public Transform playerTarget;

    

    private void Start()
    {
        pawn = GetComponent<Pawn>();
        playerTarget = GameManager.instance.objectToFollow;

        
    }

    private void Update()
    {
        if (pawn != null && playerTarget != null)
        {
            Vector3 direction = (playerTarget.position - pawn.transform.position);
            float distance = direction.magnitude;
            direction.Normalize();

            pawn.Rotate(direction, false);

            if (distance > GameManager.instance.ufoStoppingDistance)
            {
                pawn.Move(pawn.transform.forward, false);
            }
            else
            {
                pawn.Shoot();
            }
        }
    }


}
