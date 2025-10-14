using System;

[Serializable]
public class FriendData
{
    public string friendID;
    public string friendName;
    public bool isOnline;
    public string lastSeen;

    public FriendData(string id, string name, bool online)
    {
        friendID = id;
        friendName = name;
        isOnline = online;
        lastSeen = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
