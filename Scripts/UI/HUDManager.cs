// ==================================
// HUDManager.cs
// Handles UI bars for Hunger, Thirst, Stamina, Health
// ==================================

using UnityEngine;
using UnityEngine.UI;

namespace ExoPioneer.UI
{
    public class HUDManager : MonoBehaviour
    {
        [Header("Player References")]
        public ExoPioneer.PlayerSystem.SurvivalSystem survival;
        public PlayerHealth playerHealth; // Assume you have a PlayerHealth script

        [Header("UI Elements")]
        public Slider healthBar;
        public Slider hungerBar;
        public Slider thirstBar;
        public Slider staminaBar;

        void Update()
        {
            if (playerHealth != null && healthBar != null)
                healthBar.value = playerHealth.currentHealth / playerHealth.maxHealth;

            if (survival != null)
            {
                if (hungerBar != null)
                    hungerBar.value = survival.hunger / 100f;

                if (thirstBar != null)
                    thirstBar.value = survival.thirst / 100f;

                if (staminaBar != null)
                    staminaBar.value = survival.stamina / 100f;
            }
        }
    }
}
