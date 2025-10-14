using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingRecipe
{
    public string recipeName;                     // Name of the crafted item
    public List<string> requiredItems;            // Items needed for crafting
    public Item outputItem;                       // The result of the craft

    public bool CanCraft(Inventory inventory)
    {
        // Check if player has all required items
        foreach (string itemName in requiredItems)
        {
            Item item = inventory.items.Find(i => i.itemName == itemName);
            if (item == null || item.quantity <= 0)
                return false;
        }
        return true;
    }

    public void Craft(Inventory inventory)
    {
        if (CanCraft(inventory))
        {
            // Remove one of each required item
            foreach (string itemName in requiredItems)
            {
                Item item = inventory.items.Find(i => i.itemName == itemName);
                if (item != null)
                {
                    item.quantity--;
                    if (item.quantity <= 0)
                        inventory.items.Remove(item);
                }
            }

            // Add crafted item
            inventory.AddItem(new Item(outputItem.itemName, outputItem.quantity, outputItem.isConsumable));
            Debug.Log("Crafted: " + outputItem.itemName);
        }
        else
        {
            Debug.Log("Missing materials to craft " + outputItem.itemName);
        }
    }
}
