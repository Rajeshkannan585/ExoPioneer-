// ==================================
// QuestSystem.cs
// Base quest system: assign, track, complete quests
// ==================================

using UnityEngine;
using System.Collections.Generic;

namespace ExoPioneer.Quests
{
    [System.Serializable]
    public class Quest
    {
        public string questName;
        public string description;
        public bool isCompleted;
        public string rewardItem;
        public int rewardQuantity;
    }

    public class QuestSystem : MonoBehaviour
    {
        public List<Quest> activeQuests = new List<Quest>();
        public ExoPioneer.PlayerSystem.InventorySystem playerInventory;

        // Assign a new quest
        public void AddQuest(string name, string desc, string reward, int qty)
        {
            Quest q = new Quest()
            {
                questName = name,
                description = desc,
                isCompleted = false,
                rewardItem = reward,
                rewardQuantity = qty
            };
            activeQuests.Add(q);
            Debug.Log("New Quest: " + q.questName);
        }

        // Mark quest as completed and give reward
        public void CompleteQuest(string name)
        {
            Quest q = activeQuests.Find(x => x.questName == name);
            if (q != null && !q.isCompleted)
            {
                q.isCompleted = true;
                Debug.Log("Quest Completed: " + q.questName);

                if (playerInventory != null)
                    playerInventory.AddItem(q.rewardItem, q.rewardQuantity);
            }
        }

        // Check all quests in log
        public void ShowQuestLog()
        {
            foreach (var q in activeQuests)
            {
                Debug.Log((q.isCompleted ? "[✔] " : "[ ] ") + q.questName + " - " + q.description);
            }
        }
    }
}
