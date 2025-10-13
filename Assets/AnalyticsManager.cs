using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;
using System.Collections.Generic;

public class AnalyticsManager : MonoBehaviour
{
    async void Start()
    {
        // Initialize Unity Gaming Services (required for Analytics)
        await UnityServices.InitializeAsync();

        // Start collecting player analytics data
        AnalyticsService.Instance.StartDataCollection();

        // Send a custom analytics event when the game starts
        var data = new Dictionary<string, object> 
        { 
            { "event_name", "game_started" },
            { "timestamp", System.DateTime.Now.ToString() }
        };

        AnalyticsService.Instance.CustomData("custom_event", data);

        Debug.Log("✅ Analytics Initialized Successfully!");
    }
}
