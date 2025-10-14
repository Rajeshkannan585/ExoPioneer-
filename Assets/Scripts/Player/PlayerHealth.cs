using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;     // Maximum health the player can have
    private float currentHealth;       // Current health value

    void Start()
    {
        // Set starting health to max health at the beginning
        currentHealth = maxHealth;
    }

    // Function to take damage from animals or enemies
    public void TakeDamage(float amount)
    {
        // Reduce the current health
        currentHealth -= amount;

        // Clamp health so it never goes below zero
        currentHealth = Mathf.Max(currentHealth, 0f);

        Debug.Log("Player took " + amount + " damage! Current health: " + currentHealth);

        // If health is zero, trigger death
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Player death logic (you can expand later)
        Debug.Log("Player is dead!");
        // Example: disable player movement or show death screen
        // gameObject.SetActive(false);
    }

    // Optional: function to heal player
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log("Player healed! Current health: " + currentHealth);
    }
}
