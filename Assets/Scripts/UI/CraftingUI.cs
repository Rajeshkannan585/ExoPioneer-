using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CraftingUI : MonoBehaviour
{
    public CraftingManager craftingManager;   // Reference to CraftingManager
    public GameObject recipeButtonPrefab;     // Prefab for each recipe button
    public Transform recipeListParent;        // Where buttons are placed

    private List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        // Clear existing buttons
        foreach (GameObject btn in buttons)
        {
            Destroy(btn);
        }
        buttons.Clear();

        // Create a button for each recipe
        foreach (CraftingRecipe recipe in craftingManager.recipes)
        {
            GameObject button = Instantiate(recipeButtonPrefab, recipeListParent);
            buttons.Add(button);

            // Set button text
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = recipe.recipeName;

            // Add click event
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                craftingManager.CraftItem(recipe.recipeName);
                RefreshUI();
            });
        }
    }
}
