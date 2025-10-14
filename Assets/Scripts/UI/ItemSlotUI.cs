using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;                    // UI icon of the item
    public TextMeshProUGUI quantityText;  // Quantity display
    private Item item;                    // Data of this slot
    private Inventory playerInventory;    // Reference to player inventory

    // Initialize slot with data
    public void Setup(Item newItem, Inventory inventory)
    {
        item = newItem;
        playerInventory = inventory;
        icon.sprite = item.icon;
        quantityText.text = "x" + item.quantity;
    }

    // Called when player clicks/taps the slot
    public void OnUseItem()
    {
        if (item != null && playerInventory != null)
        {
            playerInventory.UseItem(item.itemName);
        }
    }
}
