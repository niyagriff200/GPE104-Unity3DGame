using UnityEngine;

public class AstronautPickup : MonoBehaviour
{
    public float scoreValue;

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
        }
    }
}
