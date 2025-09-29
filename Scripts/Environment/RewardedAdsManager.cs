// ==================================
// RewardedAdsManager.cs
// Handles rewarded ads for free coins or loot
// ==================================

using UnityEngine;
using GoogleMobileAds.Api;  // Requires AdMob SDK

namespace ExoPioneer.Monetization
{
    public class RewardedAdsManager : MonoBehaviour
    {
        private RewardedAd rewardedAd;

        [Header("AdMob Ad Unit ID")]
        public string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // Test ID

        void Start()
        {
            MobileAds.Initialize(initStatus => { });
            RequestRewardedAd();
        }

        public void RequestRewardedAd()
        {
            rewardedAd = new RewardedAd(adUnitId);

            // Events
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += (sender, args) => RequestRewardedAd();

            // Load ad
            rewardedAd.LoadAd(new AdRequest.Builder().Build());
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }
            else
            {
                Debug.Log("Rewarded ad not loaded yet.");
            }
        }

        private void HandleUserEarnedReward(object sender, Reward args)
        {
            Debug.Log("Player earned reward: " + args.Amount);

            var inv = FindObjectOfType<ExoPioneer.PlayerSystem.InventorySystem>();
            if (inv != null)
            {
                inv.AddItem("Coin", 50); // Reward 50 coins
                Debug.Log("50 Coins added to inventory!");
            }
        }
    }
}
