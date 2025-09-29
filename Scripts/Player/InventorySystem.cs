// ==================================
// InventorySystem.cs
// Simple inventory to collect, store, and use items
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.PlayerSystem
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public int quantity;

        public InventoryItem(string name, int qty)
        {
            itemName = name;
            quantity = qty;
        }
    }

    public class InventorySystem : MonoBehaviour
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        // Add item to inventory
        public void AddItem(string name, int qty)
        {
            InventoryItem existing = items.Find(i => i.itemName == name);
            if (existing != null)
            {
                existing.quantity += qty;
            }
            else
            {
                items.Add(new InventoryItem(name, qty));
            }
            Debug.Log("Added " + qty + " x " + name + " to inventory.");
        }

        // Remove item from inventory
        public bool RemoveItem(string name, int qty)
        {
            InventoryItem existing = items.Find(i => i.itemName == name);
            if (existing != null && existing.quantity >= qty)
            {
                existing.quantity -= qty;
                if (existing.quantity == 0)
                    items.Remove(existing);

                Debug.Log("Removed " + qty + " x " + name);
                return true;
            }
            Debug.Log("Not enough " + name + " in inventory!");
            return false;
        }

        // Check if item exists
        public bool HasItem(string name, int qty = 1)
        {
            InventoryItem existing = items.Find(i => i.itemName == name);
            return existing != null && existing.quantity >= qty;
        }
    }
}
