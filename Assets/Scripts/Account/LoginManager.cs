using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoginManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public Button loginButton;
    public Button logoutButton;
    public TMP_Text welcomeText;
    public CanvasGroup loginPanel;

    private PlayerAccountData accountData = new PlayerAccountData();

    void Start()
    {
        accountData.Load();
        if (accountData.isLoggedIn)
            ShowWelcome();
        else
            ShowLogin();

        loginButton.onClick.AddListener(Login);
        logoutButton.onClick.AddListener(Logout);
    }

    void Login()
    {
        if (string.IsNullOrEmpty(nameInput.text)) return;

        accountData.playerName = nameInput.text;
        accountData.playerEmail = emailInput.text;
        accountData.playerID = Guid.NewGuid().ToString();
        accountData.lastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        accountData.isLoggedIn = true;
        accountData.Save();

        // Optionally sync to Firebase cloud
        Debug.Log($"✅ Player Logged In: {accountData.playerName} ({accountData.playerEmail})");
        ShowWelcome();
    }

    void Logout()
    {
        accountData.Logout();
        Debug.Log("🚪 Player logged out.");
        ShowLogin();
    }

    void ShowLogin()
    {
        loginPanel.alpha = 1;
        loginPanel.blocksRaycasts = true;
        welcomeText.text = "";
    }

    void ShowWelcome()
    {
        loginPanel.alpha = 0;
        loginPanel.blocksRaycasts = false;
        welcomeText.text = $"👋 Welcome, {accountData.playerName}!";
    }
}
