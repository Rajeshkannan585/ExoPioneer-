// ==================================
// CraftingSystem.cs
// Handles crafting weapons, tools, items
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.PlayerSystem
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public string itemName;
        public string[] requiredResources;
    }

    public class CraftingSystem : MonoBehaviour
    {
        public List<CraftingRecipe> recipes;

        public void Craft(string itemName)
        {
            var recipe = recipes.Find(r => r.itemName == itemName);
            if (recipe != null)
            {
                Debug.Log("Crafted: " + recipe.itemName);
                // TODO: check inventory for resources
            }
            else
            {
                Debug.Log("Recipe not found!");
            }
        }
    }
}
