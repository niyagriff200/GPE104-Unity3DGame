using UnityEngine;

public class PlayerCeilingLimiter : MonoBehaviour
{
    private float maxY;
    private bool isMaxYSet = false; // Flag to check if there's a value.

    private void Update()
    {
        // If the maxY value yet, try to get it.
        if (!isMaxYSet)
        {
            // Check if the GameManager and its level data are ready.
            if (GameManager.instance != null && GameManager.instance.currentLevelData != null)
            {
                // Get the value and set our flag to true so we don't run this block again.
                maxY = GameManager.instance.currentLevelData.playerMaxY;
                isMaxYSet = true;
                Debug.Log("Player ceiling limiter is set to: " + maxY);
            }
        }

        // If the value has been set, we can now enforce the ceiling limit.
        if (isMaxYSet)
        {
            if (transform.position.y > maxY)
            {
                // Create a temporary variable, modify it, and assign it back.
                Vector3 pos = transform.position;
                pos.y = maxY;
                transform.position = pos;
            }
        }
    }
}