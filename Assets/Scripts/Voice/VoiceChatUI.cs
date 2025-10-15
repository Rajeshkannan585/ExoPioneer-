using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceChatUI : MonoBehaviour
{
    public Button muteButton;
    public Button leaveButton;
    public TMP_Text statusText;
    private bool isMuted = false;

    void Start()
    {
        muteButton.onClick.AddListener(ToggleMute);
        leaveButton.onClick.AddListener(LeaveVoice);
        statusText.text = "🎧 Voice Connected";
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        statusText.text = isMuted ? "🔇 Muted" : "🎤 Unmuted";
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    void LeaveVoice()
    {
        FindObjectOfType<VoiceChatManager>().LeaveVoiceChannel();
        statusText.text = "🚪 Disconnected from voice";
    }
}
