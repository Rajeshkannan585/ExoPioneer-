using UnityEngine;

[System.Serializable]
public class ControlSettingsData
{
    public float lookSensitivity = 1.0f;
    public float moveSensitivity = 1.0f;
    public bool invertYAxis = false;
    public string jumpKey = "Space";
    public string fireKey = "Mouse0";
    public string interactKey = "E";

    public void Save()
    {
        PlayerPrefs.SetFloat("LookSensitivity", lookSensitivity);
        PlayerPrefs.SetFloat("MoveSensitivity", moveSensitivity);
        PlayerPrefs.SetInt("InvertY", invertYAxis ? 1 : 0);
        PlayerPrefs.SetString("JumpKey", jumpKey);
        PlayerPrefs.SetString("FireKey", fireKey);
        PlayerPrefs.SetString("InteractKey", interactKey);
        PlayerPrefs.Save();
        Debug.Log("🎮 Control settings saved!");
    }

    public void Load()
    {
        lookSensitivity = PlayerPrefs.GetFloat("LookSensitivity", 1.0f);
        moveSensitivity = PlayerPrefs.GetFloat("MoveSensitivity", 1.0f);
        invertYAxis = PlayerPrefs.GetInt("InvertY", 0) == 1;
        jumpKey = PlayerPrefs.GetString("JumpKey", "Space");
        fireKey = PlayerPrefs.GetString("FireKey", "Mouse0");
        interactKey = PlayerPrefs.GetString("InteractKey", "E");
    }
}
