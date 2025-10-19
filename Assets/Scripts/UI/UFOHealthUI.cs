
using UnityEngine;
using UnityEngine.UI;

// Updates UFO health bar based on current health
public class UFOHealthUI : MonoBehaviour
{
    public Image healthFill;
    private Health health;

    private void Start()
    {
        health = GetComponentInParent<Health>();
    }

    private void Update()
    {
        if (health != null)
        {
            healthFill.fillAmount = health.GetHealthPercent();
        }
    }
}
