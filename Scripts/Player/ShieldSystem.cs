// ==================================
// ShieldSystem.cs
// Handles player energy shield (protects from damage & environment)
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class ShieldSystem : MonoBehaviour
    {
        [Header("Shield Settings")]
        public float maxShield = 100f;
        public float currentShield;
        public float rechargeRate = 5f;     // shield recharge per second
        public float rechargeDelay = 3f;    // seconds after taking damage

        private float lastHitTime;

        void Start()
        {
            currentShield = maxShield;
        }

        void Update()
        {
            // Recharge shield if enough time passed
            if (Time.time > lastHitTime + rechargeDelay && currentShield < maxShield)
            {
                currentShield += rechargeRate * Time.deltaTime;
                currentShield = Mathf.Clamp(currentShield, 0, maxShield);
            }
        }

        public void TakeShieldDamage(float dmg)
        {
            currentShield -= dmg;
            lastHitTime = Time.time;

            if (currentShield < 0)
            {
                currentShield = 0;
                Debug.Log("Shield Down! Player vulnerable.");
            }
        }

        public bool IsShieldActive()
        {
            return currentShield > 0;
        }
    }
}
