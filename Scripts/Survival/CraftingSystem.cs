// ==================================
// CraftingSystem.cs
// Allows players to craft weapons, tools, and survival gear
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.Survival
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public string itemName;
        public List<string> requiredItems;
        public List<int> requiredAmounts;
        public float craftTime = 2f;
    }

    public class CraftingSystem : MonoBehaviour
    {
        [Header("Crafting Recipes")]
        public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

        [Header("References")]
        public ExoPioneer.PlayerSystem.InventorySystem inventory;

        private bool isCrafting = false;

        public void CraftItem(string targetItem)
        {
            if (isCrafting) return;

            CraftingRecipe recipe = recipes.Find(r => r.itemName == targetItem);
            if (recipe == null)
            {
                Debug.Log("No recipe found for: " + targetItem);
                return;
            }

            // Check if player has required items
            for (int i = 0; i < recipe.requiredItems.Count; i++)
            {
                if (!inventory.HasItem(recipe.requiredItems[i], recipe.requiredAmounts[i]))
                {
                    Debug.Log("Missing item: " + recipe.requiredItems[i]);
                    return;
                }
            }

            StartCoroutine(CraftRoutine(recipe));
        }

        private System.Collections.IEnumerator CraftRoutine(CraftingRecipe recipe)
        {
            isCrafting = true;
            Debug.Log("Crafting " + recipe.itemName + "...");

            yield return new WaitForSeconds(recipe.craftTime);

            // Remove required items
            for (int i = 0; i < recipe.requiredItems.Count; i++)
            {
                inventory.RemoveItem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
            }

            // Add crafted item
            inventory.AddItem(recipe.itemName, 1);

            Debug.Log(recipe.itemName + " crafted successfully!");
            isCrafting = false;
        }
    }
}
