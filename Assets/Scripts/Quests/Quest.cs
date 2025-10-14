using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public string description;
    public int requiredAmount;
    public int currentAmount;
    public bool IsComplete => currentAmount >= requiredAmount;
}

[System.Serializable]
public class Quest
{
    public string questName;
    [TextArea(2, 4)] public string description;
    public bool isMainQuest;
    public QuestGoal[] goals;
    public int rewardCoins;
    public int rewardXP;
    public string rewardItemName;
    public bool isCompleted;
}
