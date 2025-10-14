using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
    public string currentLanguage = "en";

    private Dictionary<string, string> localizedTexts = new Dictionary<string, string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage(PlayerPrefs.GetString("Language", "en"));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLanguage(string langCode)
    {
        currentLanguage = langCode;
        localizedTexts.Clear();

        TextAsset textFile = Resources.Load<TextAsset>($"Localization/{langCode}");
        if (textFile != null)
        {
            string[] lines = textFile.text.Split('\n');
            foreach (var line in lines)
            {
                if (!line.Contains("=")) continue;
                string[] parts = line.Split('=');
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                if (!localizedTexts.ContainsKey(key))
                    localizedTexts.Add(key, value);
            }
        }
        PlayerPrefs.SetString("Language", langCode);
        PlayerPrefs.Save();
        Debug.Log($"🌐 Language loaded: {langCode}");
    }

    public string GetText(string key)
    {
        if (localizedTexts.ContainsKey(key))
            return localizedTexts[key];
        return key;
    }
}
