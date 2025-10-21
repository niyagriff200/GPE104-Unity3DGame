using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    private float scoreValue;

    private void Start()
    {
        scoreValue = GameManager.instance.astronautScore;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerPawn pawn = other.GetComponent<PlayerPawn>();
        if (pawn != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.pickupSound, transform.position, 1f);
            GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
