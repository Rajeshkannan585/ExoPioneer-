// ==================================
// HazardZone.cs
// Applies damage to player inside radiation/acid/lava zones
// ==================================

using UnityEngine;

namespace ExoPioneer.Environment
{
    public class HazardZone : MonoBehaviour
    {
        [Header("Hazard Settings")]
        public string hazardName = "Radiation Zone";
        public float damagePerSecond = 10f;

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var shield = other.GetComponent<ExoPioneer.PlayerSystem.ShieldSystem>();
                var health = other.GetComponent<ExoPioneer.PlayerSystem.PlayerHealth>();

                if (shield != null && shield.IsShieldActive())
                {
                    shield.TakeShieldDamage(damagePerSecond * Time.deltaTime);
                }
                else if (health != null)
                {
                    health.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered " + hazardName);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player exited " + hazardName);
            }
        }
    }
}
