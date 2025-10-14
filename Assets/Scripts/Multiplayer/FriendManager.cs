using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using System.Collections.Generic;

public class FriendManager : MonoBehaviour
{
    public TMP_InputField friendEmailInput;
    public TMP_Text friendListText;
    private DatabaseReference dbRef;
    private FirebaseAuth auth;
    private string userID;

    private Dictionary<string, FriendData> friends = new();

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        if (auth.CurrentUser != null)
        {
            userID = auth.CurrentUser.UserId;
            ListenForFriendUpdates();
        }
    }

    public void AddFriend()
    {
        string email = friendEmailInput.text;
        string sanitized = email.Replace(".", "_");
        dbRef.Child("users").OrderByChild("playerEmail").EqualTo(email).GetValueAsync().ContinueWith(task =>
        {
            if (task.Result.Exists)
            {
                foreach (var child in task.Result.Children)
                {
                    string friendID = child.Key;
                    string friendName = child.Child("playerName").Value.ToString();
                    FriendData newFriend = new FriendData(friendID, friendName, false);
                    string json = JsonUtility.ToJson(newFriend);
                    dbRef.Child("friends").Child(userID).Child(friendID).SetRawJsonValueAsync(json);
                    Debug.Log($"✅ Friend added: {friendName}");
                }
            }
            else
            {
                Debug.Log("⚠️ Friend not found.");
            }
        });
    }

    void ListenForFriendUpdates()
    {
        dbRef.Child("friends").Child(userID).ValueChanged += (sender, args) =>
        {
            if (args.DatabaseError != null) return;

            friends.Clear();
            foreach (var friendNode in args.Snapshot.Children)
            {
                FriendData friend = JsonUtility.FromJson<FriendData>(friendNode.GetRawJsonValue());
                friends[friend.friendID] = friend;
            }
            UpdateFriendListUI();
        };
    }

    void UpdateFriendListUI()
    {
        friendListText.text = "";
        foreach (var friend in friends.Values)
        {
            string status = friend.isOnline ? "🟢 Online" : "⚪ Offline";
            friendListText.text += $"{friend.friendName} - {status}\n";
        }
    }
}
