// ==================================
// SaveLoadManager.cs
// Handles saving & loading player progress (JSON based)
// ==================================

using UnityEngine;
using System.IO;

namespace ExoPioneer.Systems
{
    [System.Serializable]
    public class SaveData
    {
        public float playerHealth;
        public float hunger;
        public float thirst;
        public float stamina;
        public string[] inventoryItems;
    }

    public class SaveLoadManager : MonoBehaviour
    {
        private string saveFile => Application.persistentDataPath + "/save.json";

        public void SaveGame(ExoPioneer.PlayerSystem.PlayerHealth health, 
                             ExoPioneer.PlayerSystem.SurvivalSystem survival, 
                             ExoPioneer.PlayerSystem.InventorySystem inventory)
        {
            SaveData data = new SaveData();
            data.playerHealth = health.currentHealth;
            data.hunger = survival.hunger;
            data.thirst = survival.thirst;
            data.stamina = survival.stamina;

            // Save inventory as item strings
            data.inventoryItems = new string[inventory.items.Count];
            for (int i = 0; i < inventory.items.Count; i++)
            {
                data.inventoryItems[i] = inventory.items[i].itemName + ":" + inventory.items[i].quantity;
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFile, json);
            Debug.Log("Game Saved: " + saveFile);
        }

        public void LoadGame(ExoPioneer.PlayerSystem.PlayerHealth health, 
                             ExoPioneer.PlayerSystem.SurvivalSystem survival, 
                             ExoPioneer.PlayerSystem.InventorySystem inventory)
        {
            if (!File.Exists(saveFile))
            {
                Debug.Log("No save file found.");
                return;
            }

            string json = File.ReadAllText(saveFile);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Restore values
            health.currentHealth = data.playerHealth;
            survival.hunger = data.hunger;
            survival.thirst = data.thirst;
            survival.stamina = data.stamina;

            // Restore inventory
            inventory.items.Clear();
            foreach (var entry in data.inventoryItems)
            {
                string[] parts = entry.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int qty))
                {
                    inventory.AddItem(parts[0], qty);
                }
            }

            Debug.Log("Game Loaded.");
        }
    }
}
