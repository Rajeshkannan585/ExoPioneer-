using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Collections.Generic;

public class FirebaseTowerLeaderboard : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Text leaderboardText;

    DatabaseReference dbRef;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                LoadLeaderboard();
            }
        });
    }

    public void SubmitScore(int floor, int coins, float time)
    {
        string playerName = playerNameInput.text;
        string key = dbRef.Child("leaderboard").Push().Key;

        var entry = new Dictionary<string, object>
        {
            { "name", playerName },
            { "floor", floor },
            { "coins", coins },
            { "time", time }
        };

        dbRef.Child("leaderboard").Child(key).SetValueAsync(entry);
    }

    public void LoadLeaderboard()
    {
        dbRef.Child("leaderboard").OrderByChild("floor").LimitToLast(10)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    DataSnapshot snap = task.Result;
                    leaderboardText.text = "🌐 Global Tower Leaderboard\n\n";
                    foreach (DataSnapshot item in snap.Children)
                    {
                        string name = item.Child("name").Value.ToString();
                        string floor = item.Child("floor").Value.ToString();
                        string coins = item.Child("coins").Value.ToString();
                        string time = item.Child("time").Value.ToString();
                        leaderboardText.text += $"{name} - Floor {floor} | {coins}💰 | {time}s\n";
                    }
                }
            });
    }
}
