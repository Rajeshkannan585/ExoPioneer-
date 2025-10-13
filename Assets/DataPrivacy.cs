using UnityEngine;
using UnityEngine.UI;

public class DataPrivacy : MonoBehaviour
{
    [Header("Privacy Settings")]
    public GameObject privacyPanel;
    public Toggle consentToggle;
    public Button acceptButton;

    private const string consentKey = "UserPrivacyConsent";

    void Start()
    {
        // Check saved consent preference
        if (PlayerPrefs.HasKey(consentKey))
        {
            bool consentGiven = PlayerPrefs.GetInt(consentKey) == 1;
            privacyPanel.SetActive(!consentGiven);
        }
        else
        {
            privacyPanel.SetActive(true);
        }

        acceptButton.onClick.AddListener(AcceptPrivacy);
    }

    public void AcceptPrivacy()
    {
        bool consentGiven = consentToggle != null && consentToggle.isOn;
        PlayerPrefs.SetInt(consentKey, consentGiven ? 1 : 0);
        PlayerPrefs.Save();

        privacyPanel.SetActive(false);
        Debug.Log("User privacy consent saved: " + consentGiven);
    }
}
