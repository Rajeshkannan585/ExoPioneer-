using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FastTravelPoint : MonoBehaviour
{
    public string locationName;
    public Transform destinationPoint;
    public Button travelButton;
    public TextMeshProUGUI travelText;

    private bool unlocked = false;

    void Start()
    {
        travelButton.onClick.AddListener(TravelHere);
        CheckUnlock();
    }

    void CheckUnlock()
    {
        unlocked = PlayerPrefs.GetInt("Unlocked_" + locationName, 0) == 1;
        travelButton.interactable = unlocked;
        travelText.text = unlocked ? $"🛰️ {locationName}" : $"🔒 {locationName}";
    }

    public void UnlockPoint()
    {
        unlocked = true;
        PlayerPrefs.SetInt("Unlocked_" + locationName, 1);
        CheckUnlock();
    }

    public void TravelHere()
    {
        if (!unlocked) return;
        var player = GameObject.FindWithTag("Player").transform;
        player.position = destinationPoint.position;
        Debug.Log($"✅ Fast Travel: {locationName}");
    }
}
