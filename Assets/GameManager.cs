using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float health = 100f;
    public float oxygen = 100f;
    public float shield = 50f;

    public Text healthText;
    public Text oxygenText;
    public Text shieldText;

    void Update()
    {
        // Oxygen drain over time
        oxygen -= Time.deltaTime * 2f;
        if (oxygen < 0) oxygen = 0;

        // If no oxygen, health decreases
        if (oxygen <= 0)
        {
            health -= Time.deltaTime * 5f;
            if (health < 0) health = 0;
        }

        // Update UI
        healthText.text = "Health: " + Mathf.Round(health);
        oxygenText.text = "Oxygen: " + Mathf.Round(oxygen);
        shieldText.text = "Shield: " + Mathf.Round(shield);
    }

    public void AddOxygen(float amount)
    {
        oxygen = Mathf.Clamp(oxygen + amount, 0, 100);
    }

    public void Heal(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, 100);
    }

    public void AddShield(float amount)
    {
        shield = Mathf.Clamp(shield + amount, 0, 100);
    }
}
