
using UnityEngine;

// Base class for all death behaviors
public abstract class Death : MonoBehaviour
{
    // Called when health reaches zero
    public abstract void Die();
}
