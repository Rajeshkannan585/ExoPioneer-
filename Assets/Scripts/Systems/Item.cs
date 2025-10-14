using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;       // Name of the item
    public Sprite icon;           // UI icon of the item
    public int quantity;          // Quantity of the item
    public bool isConsumable;     // True if the item can be used (like food or medkit)

    public Item(string name, int qty, bool consumable)
    {
        itemName = name;
        quantity = qty;
        isConsumable = consumable;
    }
}
