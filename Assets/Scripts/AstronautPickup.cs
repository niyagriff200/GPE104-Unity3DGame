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
            GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
