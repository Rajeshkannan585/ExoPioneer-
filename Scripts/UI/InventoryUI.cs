// ==================================
// InventoryUI.cs
// Displays items from InventorySystem in UI
// ==================================

using UnityEngine;
using UnityEngine.UI;
using System.Text;
using ExoPioneer.PlayerSystem;

namespace ExoPioneer.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("References")]
        public InventorySystem playerInventory;
        public Text inventoryText; // Simple text display (later replace with slots/buttons)

        void Update()
        {
            if (playerInventory != null && inventoryText != null)
            {
                inventoryText.text = BuildInventoryString();
            }
        }

        string BuildInventoryString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in playerInventory.items)
            {
                sb.AppendLine(item.itemName + " x " + item.quantity);
            }
            return sb.ToString();
        }
    }
}
