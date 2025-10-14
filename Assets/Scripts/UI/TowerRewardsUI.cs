using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TowerRewardsUI : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup rewardPanel;
    public TextMeshProUGUI floorText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI timeText;
    public Button continueButton;

    private float startTime;
    private float clearTime;

    void Start()
    {
        rewardPanel.alpha = 0;
        rewardPanel.interactable = false;
        rewardPanel.blocksRaycasts = false;

        continueButton.onClick.AddListener(HideRewards);
        startTime = Time.time;
    }

    public void ShowRewards(int floor, int coins, int xp)
    {
        clearTime = Time.time - startTime;

        floorText.text = $"🏰 Floor {floor + 1} Cleared!";
        coinsText.text = $"💰 Coins Earned: {coins}";
        xpText.text = $"⭐ XP Gained: {xp}";
        timeText.text = $"⏱️ Clear Time: {clearTime:F1} sec";

        StartCoroutine(FadeIn());

        // Save data to leaderboard
        TowerLeaderboardManager.SaveScore(floor + 1, coins, clearTime);
    }

    IEnumerator FadeIn()
    {
        rewardPanel.blocksRaycasts = true;
        rewardPanel.interactable = true;
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rewardPanel.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }
        rewardPanel.alpha = 1;
    }

    void HideRewards()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            rewardPanel.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }
        rewardPanel.alpha = 0;
        rewardPanel.interactable = false;
        rewardPanel.blocksRaycasts = false;
    }
}
