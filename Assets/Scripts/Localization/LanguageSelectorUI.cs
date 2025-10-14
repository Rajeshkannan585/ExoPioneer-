using UnityEngine;
using UnityEngine.UI;

public class LanguageSelectorUI : MonoBehaviour
{
    public Button englishButton;
    public Button tamilButton;

    void Start()
    {
        englishButton.onClick.AddListener(() => SetLanguage("en"));
        tamilButton.onClick.AddListener(() => SetLanguage("ta"));
    }

    void SetLanguage(string code)
    {
        LocalizationManager.Instance.LoadLanguage(code);
        foreach (var text in FindObjectsOfType<LocalizedText>())
        {
            text.UpdateText();
        }
        Debug.Log("🌍 Language changed to " + code);
    }
}
