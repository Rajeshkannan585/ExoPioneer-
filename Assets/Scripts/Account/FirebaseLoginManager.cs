using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;

public class FirebaseLoginManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    private FirebaseAuth auth;
    private DatabaseReference dbRef;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("✅ Firebase initialized.");
            }
            else
            {
                Debug.LogError("❌ Firebase init failed: " + task.Result);
            }
        });
    }

    public void Register()
    {
        string email = emailInput.text;
        string pass = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                statusText.text = "⚠️ Registration failed!";
                return;
            }

            FirebaseUser newUser = task.Result;
            statusText.text = "✅ Registered successfully: " + newUser.Email;

            PlayerAccountData account = new PlayerAccountData
            {
                playerName = newUser.Email.Split('@')[0],
                playerEmail = newUser.Email,
                playerID = newUser.UserId,
                lastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                isLoggedIn = true
            };
            account.Save();

            SaveToCloud(account);
        });
    }

    public void Login()
    {
        string email = emailInput.text;
        string pass = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                statusText.text = "⚠️ Login failed!";
                return;
            }

            FirebaseUser user = task.Result;
            statusText.text = "👋 Welcome back, " + user.Email;
            LoadFromCloud(user.UserId);
        });
    }

    public void SaveToCloud(PlayerAccountData data)
    {
        string json = JsonUtility.ToJson(data);
        dbRef.Child("users").Child(data.playerID).SetRawJsonValueAsync(json);
        statusText.text = "☁️ Data uploaded to cloud!";
    }

    public void LoadFromCloud(string playerID)
    {
        dbRef.Child("users").Child(playerID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                statusText.text = "⚠️ Failed to load from cloud!";
            }
            else if (task.Result.Exists)
            {
                string json = task.Result.GetRawJsonValue();
                PlayerAccountData account = JsonUtility.FromJson<PlayerAccountData>(json);
                account.Save();
                statusText.text = "☁️ Data synced from cloud!";
            }
        });
    }
}
