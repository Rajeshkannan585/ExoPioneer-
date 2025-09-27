using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float playerHealth = 100f;
    public float playerOxygen = 100f;
    public float playerShield = 50f;

    public Text healthText;
    public Text oxygenText;
    public Text shieldText;

    void Update()
    {
        // Oxygen drain over time
        playerOxygen -= Time.deltaTime * 2f; // 2 units per second

        // If oxygen empty, reduce health
        if (playerOxygen <= 0)
        {
            playerHealth -= Time.deltaTime * 5f; // health damage when no oxygen
            playerOxygen = 0;
        }

        // Clamp values (never go below 0 or above 100)
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        playerOxygen = Mathf.Clamp(playerOxygen, 0, 100);
        playerShield = Mathf.Clamp(playerShield, 0, 100);

        // Update UI (if assigned in Inspector)
        if (healthText != null) healthText.text = "Health: " + playerHealth.ToString("F0");
        if (oxygenText != null) oxygenText.text = "Oxygen: " + playerOxygen.ToString("F0");
        if (shieldText != null) shieldText.text = "Shield: " + playerShield.ToString("F0");
    }

    public void TakeDamage(float damage)
    {
        if (playerShield > 0)
        {
            playerShield -= damage;
        }
        else
        {
            playerHealth -= damage;
        }
    }

    public void RefillOxygen(float amount)
    {
        playerOxygen += amount;
    }

    public void Heal(float amount)
    {
        playerHealth += amount;
    }
}
