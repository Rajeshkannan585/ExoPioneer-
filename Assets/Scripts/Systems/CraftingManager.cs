using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public Inventory playerInventory;            // Reference to player's inventory
    public List<CraftingRecipe> recipes;         // List of available recipes

    public void CraftItem(string recipeName)
    {
        CraftingRecipe recipe = recipes.Find(r => r.recipeName == recipeName);
        if (recipe != null)
        {
            recipe.Craft(playerInventory);
        }
        else
        {
            Debug.Log("No recipe found with name: " + recipeName);
        }
    }
}
