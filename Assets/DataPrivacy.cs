using UnityEngine;
using UnityEngine.UI;

public class DataPrivacy : MonoBehaviour
{
    [Header("Privacy Settings")]
    public GameObject privacyPanel;       // Panel UI for privacy popup
    public Toggle consentToggle;          // Toggle for user consent
    public Button acceptButton;           // Button to confirm consent

    private const string consentKey = "UserPrivacyConsent"; // PlayerPrefs key

    void Start()
    {
        // Check if the user already accepted the privacy policy
        if (PlayerPrefs.HasKey(consentKey))
        {
            bool consentGiven = PlayerPrefs.GetInt(consentKey) == 1;
            privacyPanel.SetActive(!consentGiven);  // Hide panel if already accepted
        }
        else
        {
            // Show panel first time only
            privacyPanel.SetActive(true);
        }

        // Button listener to save consent
        acceptButton.onClick.AddListener(AcceptPrivacy);
    }

    // Called when player accepts privacy settings
    public void AcceptPrivacy()
    {
        if (consentToggle != null)
        {
            bool consentGiven = consentToggle.isOn;
            PlayerPrefs.SetInt(consentKey, consentGiven ? 1 : 0);
            PlayerPrefs.Save();

            privacyPanel.SetActive(false);
            Debug.Log("✅ User privacy consent saved: " + consentGiven);
        }
        else
        {
            Debug.LogWarning("⚠️ Consent toggle not assigned in Inspector!");
        }
    }
}
