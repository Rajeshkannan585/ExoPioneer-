using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class TowerScore
{
    public int floor;
    public int coins;
    public float time;
}

public class TowerLeaderboardManager : MonoBehaviour
{
    public Transform leaderboardContainer;
    public GameObject entryPrefab;

    private static List<TowerScore> scores = new List<TowerScore>();

    public static void SaveScore(int floor, int coins, float time)
    {
        TowerScore s = new TowerScore() { floor = floor, coins = coins, time = time };
        scores.Add(s);

        // Save to PlayerPrefs for persistence
        string data = JsonUtility.ToJson(new ScoreListWrapper(scores));
        PlayerPrefs.SetString("TowerScores", data);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        leaderboardContainer.DestroyAllChildren();

        string data = PlayerPrefs.GetString("TowerScores", "");
        if (!string.IsNullOrEmpty(data))
        {
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(data);
            scores = wrapper.scores;
        }

        var ordered = scores.OrderByDescending(s => s.floor).ThenBy(s => s.time);

        foreach (var score in ordered.Take(10))
        {
            GameObject entry = Instantiate(entryPrefab, leaderboardContainer);
            entry.GetComponentInChildren<TextMeshProUGUI>().text =
                $"Floor {score.floor}  |  Coins: {score.coins}  |  Time: {score.time:F1}s";
        }
    }

    [System.Serializable]
    public class ScoreListWrapper
    {
        public List<TowerScore> scores;
        public ScoreListWrapper(List<TowerScore> list) { scores = list; }
    }
}

public static class TransformExtensions
{
    public static void DestroyAllChildren(this Transform t)
    {
        foreach (Transform child in t)
            GameObject.Destroy(child.gameObject);
    }
}
