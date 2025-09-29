using UnityEngine;

namespace ExoPioneer.Animals
{
    public class AnimalHealth : MonoBehaviour
    {
        public int maxHealth = 100; // Maximum health of the animal
        private int currentHealth;

        void Start()
        {
            currentHealth = maxHealth; // Initialize health
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount; // Reduce health
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log(gameObject.name + " has been hunted!"); 
            // TODO: Add loot drop or resource system here
            Destroy(gameObject); // Remove animal from scene
        }
    }
}
