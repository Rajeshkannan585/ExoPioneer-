using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AnalyticsManager : MonoBehaviour
{
    async void Awake()
    {
        await InitializeAnalytics();
    }

    private async Task InitializeAnalytics()
    {
        try
        {
            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();

            var data = new Dictionary<string, object>
            {
                { "event_name", "game_started" },
                { "timestamp", System.DateTime.Now.ToString() }
            };
            AnalyticsService.Instance.CustomData("custom_event", data);

            Debug.Log("✅ Analytics Initialized Successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Analytics Initialization Failed: " + e.Message);
        }
    }
}
