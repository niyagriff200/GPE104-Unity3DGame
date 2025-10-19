using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            Vector3 direction = (playerTarget.position - pawn.transform.position).normalized;
            pawn.Move(direction, false);

            
            //pawn.Fire();
        }
    }


}
