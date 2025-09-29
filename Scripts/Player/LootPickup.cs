// ==================================
// LootPickup.cs
// Allows player to pick up items from the ground
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class LootPickup : MonoBehaviour
    {
        [Header("Loot Settings")]
        public string itemName = "Default Item"; // Name of item
        public int quantity = 1;                 // How many items

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventorySystem inv = other.GetComponent<InventorySystem>();
                if (inv != null)
                {
                    inv.AddItem(itemName, quantity);
                    Debug.Log("Picked up: " + quantity + " x " + itemName);
                    Destroy(gameObject); // Remove loot object after pickup
                }
            }
        }
    }
}
