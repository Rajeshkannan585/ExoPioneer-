using UnityEngine;

[System.Serializable]
public class PlayerAccountData
{
    public string playerName;
    public string playerEmail;
    public string playerID;
    public string lastLoginDate;
    public bool isLoggedIn;

    public void Save()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("PlayerEmail", playerEmail);
        PlayerPrefs.SetString("PlayerID", playerID);
        PlayerPrefs.SetString("LastLogin", lastLoginDate);
        PlayerPrefs.SetInt("IsLoggedIn", isLoggedIn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("👤 Account saved locally.");
    }

    public void Load()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "");
        playerEmail = PlayerPrefs.GetString("PlayerEmail", "");
        playerID = PlayerPrefs.GetString("PlayerID", "");
        lastLoginDate = PlayerPrefs.GetString("LastLogin", "");
        isLoggedIn = PlayerPrefs.GetInt("IsLoggedIn", 0) == 1;
    }

    public void Logout()
    {
        isLoggedIn = false;
        PlayerPrefs.SetInt("IsLoggedIn", 0);
        PlayerPrefs.Save();
    }
}
