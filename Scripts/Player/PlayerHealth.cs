// ==================================
// PlayerHealth.cs
// Handles player health, damage, healing
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Health Settings")]
        public float maxHealth = 100f;  // Maximum HP
        public float currentHealth;     // Current HP

        void Start()
        {
            currentHealth = maxHealth; // Initialize health
        }

        public void TakeDamage(float dmg)
        {
            currentHealth -= dmg;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            Debug.Log("Player took " + dmg + " damage. Remaining: " + currentHealth);

            if (currentHealth <= 0)
                Die();
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            Debug.Log("Player healed. Current: " + currentHealth);
        }

        void Die()
        {
            Debug.Log("Player has died!");
            // TODO: Respawn system or Game Over screen
        }
    }
}
