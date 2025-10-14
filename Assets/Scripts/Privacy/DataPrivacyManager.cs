using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class DataPrivacyManager : MonoBehaviour
{
    public CanvasGroup consentPopup;
    public Button acceptButton;
    public Button declineButton;
    public TMP_Text messageText;
    public string policyURL = "https://exopioneer-official.web.app/privacy";

    private const string ConsentKey = "PlayerConsentGiven";

    void Start()
    {
        if (PlayerPrefs.GetInt(ConsentKey, 0) == 0)
        {
            ShowConsentPopup();
        }
        else
        {
            consentPopup.alpha = 0;
            consentPopup.blocksRaycasts = false;
        }

        acceptButton.onClick.AddListener(AcceptConsent);
        declineButton.onClick.AddListener(DeclineConsent);
    }

    void ShowConsentPopup()
    {
        consentPopup.alpha = 1;
        consentPopup.blocksRaycasts = true;

        messageText.text = 
        "🧠 Data Privacy Notice:\n" +
        "We use local saves and optional cloud sync to store your game progress.\n" +
        "No personal data is collected. You can change this anytime in Privacy Settings.\n\n" +
        "நாங்கள் உங்களின் தனியுரிமையை மதிக்கிறோம் 💖";
    }

    void AcceptConsent()
    {
        PlayerPrefs.SetInt(ConsentKey, 1);
        PlayerPrefs.Save();
        consentPopup.alpha = 0;
        consentPopup.blocksRaycasts = false;
        Debug.Log("✅ Player consent accepted");
    }

    void DeclineConsent()
    {
        PlayerPrefs.SetInt(ConsentKey, -1);
        PlayerPrefs.Save();
        consentPopup.alpha = 0;
        consentPopup.blocksRaycasts = false;
        Application.OpenURL(policyURL);
        Debug.Log("🚫 Player declined data consent");
    }
}
