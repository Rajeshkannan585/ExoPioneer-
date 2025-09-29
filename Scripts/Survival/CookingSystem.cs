// ==================================
// CookingSystem.cs
// Handles cooking raw items into cooked food with hunger restore
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.Survival
{
    [System.Serializable]
    public class Recipe
    {
        public string rawItem;
        public string cookedItem;
        public float cookTime = 5f;
        public int hungerRestore = 20;
    }

    public class CookingSystem : MonoBehaviour
    {
        [Header("Cooking Recipes")]
        public List<Recipe> recipes = new List<Recipe>();

        [Header("References")]
        public ExoPioneer.PlayerSystem.InventorySystem inventory;
        public ExoPioneer.PlayerSystem.SurvivalSystem survival;

        private bool isCooking = false;

        public void Cook(string rawItemName)
        {
            if (isCooking) return;

            Recipe recipe = recipes.Find(r => r.rawItem == rawItemName);
            if (recipe == null)
            {
                Debug.Log("No recipe found for: " + rawItemName);
                return;
            }

            if (!inventory.HasItem(rawItemName, 1))
            {
                Debug.Log("You don't have " + rawItemName);
                return;
            }

            StartCoroutine(CookRoutine(recipe));
        }

        private System.Collections.IEnumerator CookRoutine(Recipe recipe)
        {
            isCooking = true;
            Debug.Log("Cooking " + recipe.rawItem + "...");

            yield return new WaitForSeconds(recipe.cookTime);

            inventory.RemoveItem(recipe.rawItem, 1);
            inventory.AddItem(recipe.cookedItem, 1);

            if (survival != null)
                survival.RestoreHunger(recipe.hungerRestore);

            Debug.Log(recipe.cookedItem + " is ready! Hunger restored.");
            isCooking = false;
        }
    }
}
