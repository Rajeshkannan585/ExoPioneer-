using UnityEngine;
using VivoxUnity;
using System;

public class VoiceChatManager : MonoBehaviour
{
    private Client vivoxClient;
    private ILoginSession loginSession;
    private IChannelSession channelSession;

    public string playerName = "Player_" + UnityEngine.Random.Range(1000,9999);
    public string channelName = "ExoPioneerVoice";

    void Start()
    {
        InitializeVivox();
    }

    void InitializeVivox()
    {
        vivoxClient = new Client();
        vivoxClient.Initialize();
        Debug.Log("🎙️ Vivox Initialized");

        loginSession = vivoxClient.GetLoginSession(vivoxClient.GetAccount(playerName));
        loginSession.BeginLogin("https://mt1s.www.vivox.com/api2",
            loginSession.LoginSessionId.ToString(),
            "token", // Replace with server token
            null, null, result =>
        {
            if (result == null)
            {
                Debug.Log("✅ Voice Login Successful");
                JoinVoiceChannel();
            }
            else
            {
                Debug.LogError("❌ Voice Login Failed: " + result.Message);
            }
        });
    }

    void JoinVoiceChannel()
    {
        channelSession = loginSession.GetChannelSession(vivoxClient.GetChannel(channelName, ChannelType.NonPositional));
        channelSession.BeginConnect(true, false, true, channelName, null, null);
        Debug.Log($"🔊 Joined Voice Channel: {channelName}");
    }

    public void LeaveVoiceChannel()
    {
        if (channelSession != null)
        {
            channelSession.Disconnect();
            Debug.Log("🚪 Left voice channel.");
        }
    }

    void OnApplicationQuit()
    {
        LeaveVoiceChannel();
        vivoxClient.Uninitialize();
    }
}
