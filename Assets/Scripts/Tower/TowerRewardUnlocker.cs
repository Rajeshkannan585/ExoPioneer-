using UnityEngine;

[System.Serializable]
public class UnlockReward
{
    public string rewardName;
    public string rewardType; // "Pet", "Weapon", "Armor"
    public Sprite rewardIcon;
    public int unlockFloor;
    public string description;
}

public class TowerRewardUnlocker : MonoBehaviour
{
    [Header("Reward List")]
    public UnlockReward[] rewards;

    [Header("UI Display")]
    public GameObject unlockPopup;
    public UnityEngine.UI.Image iconImage;
    public TMPro.TextMeshProUGUI rewardNameText;
    public TMPro.TextMeshProUGUI rewardDescText;

    void Start()
    {
        unlockPopup.SetActive(false);
    }

    public void CheckUnlock(int clearedFloor)
    {
        foreach (var r in rewards)
        {
            if (r.unlockFloor == clearedFloor)
            {
                UnlockRewardItem(r);
                break;
            }
        }
    }

    void UnlockRewardItem(UnlockReward r)
    {
        Debug.Log($"🎁 Unlocked: {r.rewardName} ({r.rewardType})");

        // Save reward permanently
        PlayerPrefs.SetInt("Unlocked_" + r.rewardName, 1);

        // Show UI popup
        unlockPopup.SetActive(true);
        iconImage.sprite = r.rewardIcon;
        rewardNameText.text = $"Unlocked: {r.rewardName}";
        rewardDescText.text = r.description;

        // Hide after 5 seconds
        Invoke(nameof(HidePopup), 5f);
    }

    void HidePopup()
    {
        unlockPopup.SetActive(false);
    }
}
