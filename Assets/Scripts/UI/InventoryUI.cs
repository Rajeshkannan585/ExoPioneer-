using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;       // Reference to player's Inventory
    public GameObject itemSlotPrefab;       // Prefab for each item slot
    public Transform itemSlotParent;        // Parent object (Grid Layout Group)

    private List<GameObject> itemSlots = new List<GameObject>();

    void Start()
    {
        // Clear existing slots at start
        foreach (Transform child in itemSlotParent)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        // Clear old slots
        foreach (GameObject slot in itemSlots)
        {
            Destroy(slot);
        }
        itemSlots.Clear();

        // Create new slots for each item in inventory
        foreach (Item item in playerInventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemSlotParent);
            itemSlots.Add(slot);

            // Set icon and quantity
            Image icon = slot.transform.Find("Icon").GetComponent<Image>();
            TextMeshProUGUI qtyText = slot.transform.Find("QtyText").GetComponent<TextMeshProUGUI>();

            icon.sprite = item.icon;
            qtyText.text = "x" + item.quantity;
        }
    }
}
