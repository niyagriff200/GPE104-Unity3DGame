
using UnityEngine;

// Handles death animation by spinning and shrinking before destroying the object
public class DeathSpin : Death
{
    private bool isDying = false;     // Tracks whether death animation has started
    private float timer = 0f;         // Tracks elapsed time since death began

    private float spinRate = 360f;    // Rotation speed in degrees per second
    private float scaleRate = 0.5f;   // Shrink rate per second
    private float duration = 1.5f;    // Max time before object is destroyed

    public override void Die()
    {
        isDying = true; // Trigger death animation
    }

    private void Update()
    {
        if (!isDying) return; // Skip if not dying

        timer += Time.deltaTime; // Track time since death started

        transform.Rotate(0f, 0f, spinRate * Time.deltaTime); // Apply spin

        transform.localScale -= Vector3.one * scaleRate * Time.deltaTime; // Shrink object

        // Destroy object when time runs out or it's nearly invisible
        if (timer >= duration || transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
