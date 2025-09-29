// ==========================================
// CostumeManager.cs
// This script manages multiple costumes/outfits 
// for the player (e.g., armor, suits, skins).
// ==========================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class CostumeManager : MonoBehaviour
    {
        [Header("Available Costumes")]
        public GameObject[] costumes; // All costume prefabs should be assigned here

        private GameObject currentCostume; // The currently equipped costume
        private int currentIndex = 0;      // Tracks which costume is active

        void Start()
        {
            // At game start, automatically equip the first costume
            if (costumes.Length > 0)
            {
                EquipCostume(0);
            }
        }

        /// <summary>
        /// Equip a costume by index
        /// </summary>
        public void EquipCostume(int index)
        {
            if (index < 0 || index >= costumes.Length)
                return;

            // Remove the old costume if it exists
            if (currentCostume != null)
                Destroy(currentCostume);

            // Spawn the new costume as a child of the player
            currentCostume = Instantiate(costumes[index], transform);
            currentIndex = index;
        }

        /// <summary>
        /// Switch to the next costume in the list
        /// </summary>
        public void NextCostume()
        {
            int nextIndex = (currentIndex + 1) % costumes.Length;
            EquipCostume(nextIndex);
        }

        /// <summary>
        /// Switch to the previous costume in the list
        /// </summary>
        public void PreviousCostume()
        {
            int prevIndex = (currentIndex - 1 + costumes.Length) % costumes.Length;
            EquipCostume(prevIndex);
        }
    }
}
