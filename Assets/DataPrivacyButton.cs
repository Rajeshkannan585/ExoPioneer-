using UnityEngine;
using UnityEngine.UI;

public class DataPrivacyButton : MonoBehaviour
{
    [Header("UI Button Settings")]
    public Button privacyButton;
    public GameObject privacyPanel;

    void Start()
    {
        if (privacyButton != null)
        {
            privacyButton.onClick.AddListener(OpenPrivacyPanel);
        }
    }

    public void OpenPrivacyPanel()
    {
        if (privacyPanel != null)
        {
            privacyPanel.SetActive(true);
            Debug.Log("Privacy panel opened by user.");
        }
        else
        {
            Debug.LogWarning("Privacy panel not assigned in inspector.");
        }
    }
}
