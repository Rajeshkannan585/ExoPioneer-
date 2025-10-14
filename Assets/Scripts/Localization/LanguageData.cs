using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LanguageData
{
    public string languageCode; // "en", "ta"
    public Dictionary<string, string> texts;

    public LanguageData(string code)
    {
        languageCode = code;
        texts = new Dictionary<string, string>();
    }
}
