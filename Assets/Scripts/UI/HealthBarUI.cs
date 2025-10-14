using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public PlayerHealth playerHealth;     // Reference to PlayerHealth script
    public Image healthBarFill;           // The UI image for the fill

    void Update()
    {
        if (playerHealth != null && healthBarFill != null)
        {
            // Calculate fill amount based on player's current health
            float fillValue = playerHealth.GetCurrentHealth() / playerHealth.maxHealth;
            healthBarFill.fillAmount = fillValue;
        }
    }
}
