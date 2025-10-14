using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;    // Reference to the inventory UI panel
    private bool isInventoryOpen = false;

    void Start()
    {
        // Hide inventory at start
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // Keyboard toggle (for PC)
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
#endif
#if UNITY_ANDROID || UNITY_IOS
        // Mobile can use a UI button (call ToggleInventory via OnClick)
#endif
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        Debug.Log("Inventory " + (isInventoryOpen ? "Opened" : "Closed"));
    }
}
