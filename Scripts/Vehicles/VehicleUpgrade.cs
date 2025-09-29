// ==================================
// VehicleUpgrade.cs
// Handles vehicle upgrades: speed, armor, shield, fuel
// ==================================

using UnityEngine;

namespace ExoPioneer.Vehicles
{
    [System.Serializable]
    public class VehicleUpgradeLevel
    {
        public string upgradeName;
        public float speedMultiplier = 1f;
        public float armorBonus = 0f;
        public float shieldBonus = 0f;
        public float fuelEfficiency = 1f;
        public int cost = 100;
    }

    public class VehicleUpgrade : MonoBehaviour
    {
        [Header("Upgrade Levels")]
        public VehicleUpgradeLevel[] upgradeLevels;
        private int currentLevel = 0;

        [Header("Vehicle Stats")]
        public float baseSpeed = 20f;
        public float baseArmor = 100f;
        public float baseShield = 50f;

        public float currentSpeed;
        public float currentArmor;
        public float currentShield;

        void Start()
        {
            ApplyUpgrade(currentLevel);
        }

        public void UpgradeVehicle()
        {
            if (currentLevel < upgradeLevels.Length - 1)
            {
                currentLevel++;
                ApplyUpgrade(currentLevel);
                Debug.Log("Vehicle upgraded to level: " + currentLevel);
            }
            else
            {
                Debug.Log("Vehicle is already max level!");
            }
        }

        void ApplyUpgrade(int levelIndex)
        {
            var u = upgradeLevels[levelIndex];

            currentSpeed = baseSpeed * u.speedMultiplier;
            currentArmor = baseArmor + u.armorBonus;
            currentShield = baseShield + u.shieldBonus;

            Debug.Log("Applied upgrade: " + u.upgradeName);
        }
    }
}
