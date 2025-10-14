using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<Quest> availableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    [Header("UI References")]
    public GameObject questLogPanel;
    public Transform questListContainer;
    public GameObject questEntryPrefab;
    public TextMeshProUGUI activeQuestText;

    void Start()
    {
        questLogPanel.SetActive(false);
        UpdateActiveQuestUI();
    }

    public void AddQuest(Quest newQuest)
    {
        if (!activeQuests.Contains(newQuest))
        {
            activeQuests.Add(newQuest);
            Debug.Log($"🪐 Quest started: {newQuest.questName}");
            UpdateQuestLogUI();
            UpdateActiveQuestUI();
        }
    }

    public void CompleteGoal(Quest quest, int goalIndex)
    {
        quest.goals[goalIndex].currentAmount++;
        Debug.Log($"Progress: {quest.questName} → {quest.goals[goalIndex].description} ({quest.goals[goalIndex].currentAmount}/{quest.goals[goalIndex].requiredAmount})");

        if (quest.goals[goalIndex].IsComplete)
        {
            CheckQuestCompletion(quest);
        }
        UpdateActiveQuestUI();
    }

    void CheckQuestCompletion(Quest quest)
    {
        foreach (var goal in quest.goals)
        {
            if (!goal.IsComplete)
                return;
        }

        quest.isCompleted = true;
        RewardPlayer(quest);
        activeQuests.Remove(quest);
        Debug.Log($"✅ Quest completed: {quest.questName}");
        UpdateQuestLogUI();
    }

    void RewardPlayer(Quest quest)
    {
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + quest.rewardCoins);
        PlayerPrefs.SetInt("PlayerXP", PlayerPrefs.GetInt("PlayerXP", 0) + quest.rewardXP);
        PlayerPrefs.SetInt("Unlocked_" + quest.rewardItemName, 1);
        Debug.Log($"🎁 Reward: {quest.rewardCoins} coins, {quest.rewardXP} XP, Item: {quest.rewardItemName}");
    }

    public void ToggleQuestLog()
    {
        questLogPanel.SetActive(!questLogPanel.activeSelf);
        if (questLogPanel.activeSelf) UpdateQuestLogUI();
    }

    void UpdateQuestLogUI()
    {
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        foreach (var quest in activeQuests)
        {
            GameObject entry = Instantiate(questEntryPrefab, questListContainer);
            entry.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{quest.questName}\n<size=70%>{quest.description}</size>";
        }
    }

    void UpdateActiveQuestUI()
    {
        if (activeQuests.Count > 0)
        {
            var q = activeQuests[0];
            string goalText = q.goals.Length > 0 ? q.goals[0].description : "";
            activeQuestText.text = $"📜 {q.questName}\n<size=70%>{goalText}</size>";
        }
        else
        {
            activeQuestText.text = "No Active Quests 🌌";
        }
    }
}
