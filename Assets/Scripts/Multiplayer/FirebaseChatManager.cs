using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using System;

public class FirebaseChatManager : MonoBehaviour
{
    public TMP_InputField messageInput;
    public TMP_Text chatOutput;
    private DatabaseReference dbRef;
    private FirebaseAuth auth;
    private string userID;
    private string currentChatFriendID;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        userID = auth.CurrentUser.UserId;
    }

    public void OpenChat(string friendID)
    {
        currentChatFriendID = friendID;
        chatOutput.text = "💬 Chat opened...\n";

        dbRef.Child("chats").Child(GetChatID()).ValueChanged += (s, e) =>
        {
            chatOutput.text = "";
            foreach (var msg in e.Snapshot.Children)
            {
                string sender = msg.Child("senderName").Value.ToString();
                string text = msg.Child("messageText").Value.ToString();
                chatOutput.text += $"{sender}: {text}\n";
            }
        };
    }

    public void SendMessage()
    {
        if (string.IsNullOrEmpty(messageInput.text)) return;

        string chatID = GetChatID();
        var msgData = new
        {
            senderID = userID,
            senderName = auth.CurrentUser.Email.Split('@')[0],
            messageText = messageInput.text,
            timestamp = DateTime.Now.ToString("HH:mm:ss")
        };
        dbRef.Child("chats").Child(chatID).Push().SetRawJsonValueAsync(JsonUtility.ToJson(msgData));
        messageInput.text = "";
    }

    private string GetChatID()
    {
        return userID.CompareTo(currentChatFriendID) < 0
            ? $"{userID}_{currentChatFriendID}"
            : $"{currentChatFriendID}_{userID}";
    }
}
