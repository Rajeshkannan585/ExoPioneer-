using UnityEngine;
using TMPro;

[ExecuteAlways]
public class LocalizedText : MonoBehaviour
{
    public string textKey;
    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (LocalizationManager.Instance != null)
            textComponent.text = LocalizationManager.Instance.GetText(textKey);
    }
}
