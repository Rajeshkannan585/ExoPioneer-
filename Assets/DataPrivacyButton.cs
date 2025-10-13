using UnityEngine;
using UnityEngine.UI;

public class DataPrivacyButton : MonoBehaviour
{
    [Header("UI Button Settings")]
    public Button privacyButton;     // Button to open the privacy panel
    public GameObject privacyPanel;  // The panel that shows privacy info

    void Start()
    {
        if (privacyButton != null)
        {
            // Add button click listener
            privacyButton.onClick.AddListener(OpenPrivacyPanel);
        }
        else
        {
            Debug.LogWarning("⚠️ Privacy button not assigned in inspector!");
        }
    }

    // Function to open the privacy panel
    public void OpenPrivacyPanel()
    {
        if (privacyPanel != null)
        {
            privacyPanel.SetActive(true);
            Debug.Log("✅ Privacy panel opened by user.");
        }
        else
        {
            Debug.LogWarning("⚠️ Privacy panel not assigned in inspector!");
        }
    }
}
