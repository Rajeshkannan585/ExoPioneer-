// ==================================
// SurvivalSystem.cs
// Tracks player Hunger, Thirst, Stamina
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class SurvivalSystem : MonoBehaviour
    {
        public float hunger = 100f;   // Decreases over time
        public float thirst = 100f;   // Decreases faster than hunger
        public float stamina = 100f;  // Decreases when running

        public float hungerDecay = 0.2f;
        public float thirstDecay = 0.5f;
        public float staminaDecay = 5f;

        void Update()
        {
            // Hunger & Thirst decay slowly
            hunger -= hungerDecay * Time.deltaTime;
            thirst -= thirstDecay * Time.deltaTime;

            // Clamp values
            hunger = Mathf.Clamp(hunger, 0, 100);
            thirst = Mathf.Clamp(thirst, 0, 100);
            stamina = Mathf.Clamp(stamina, 0, 100);

            if (hunger <= 0 || thirst <= 0)
            {
                Debug.Log("Player is starving/dehydrated!");
                // TODO: Apply health penalty
            }
        }

        public void EatFood(float amount) => hunger = Mathf.Clamp(hunger + amount, 0, 100);
        public void DrinkWater(float amount) => thirst = Mathf.Clamp(thirst + amount, 0, 100);
        public void UseStamina(float amount) => stamina = Mathf.Clamp(stamina - amount, 0, 100);
    }
}
