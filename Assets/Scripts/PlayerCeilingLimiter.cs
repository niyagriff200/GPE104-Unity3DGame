using UnityEngine;

public class PlayerCeilingLimiter : MonoBehaviour
{
    private float maxY;

    private void Start()
    {
        if (GameManager.instance != null && GameManager.instance.currentLevelData != null)
        {
            maxY = GameManager.instance.currentLevelData.playerMaxY;
        }
        else
        {
            Debug.LogWarning("LevelData not assigned in GameManager.");
        }
    }

    private void Update()
    {
        if (GameManager.instance != null && GameManager.instance.currentLevelData != null)
        {
            Vector3 position = transform.position;

            if (position.y > maxY)
            {
                position.y = maxY;
                transform.position = position;
            }
        }

        
    }
}
