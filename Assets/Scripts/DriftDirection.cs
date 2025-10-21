using UnityEngine;

// Handles internal drifting direction logic for floating objects
public class DriftDirection : MonoBehaviour
{
    private Vector3 currentDirection;
    private float directionChangeInterval;
    private float timer;

    private void Start()
    {
        directionChangeInterval = GameManager.instance.meteorDriftInterval;
        SetRandomDirection();
        timer = directionChangeInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SetRandomDirection();
            timer = directionChangeInterval;
        }
    }

    private void SetRandomDirection()
    {
        currentDirection = Random.onUnitSphere.normalized;
    }

    // Internal accessor for movement scripts
    public Vector3 GetDirection()
    {
        return currentDirection;
    }
}
