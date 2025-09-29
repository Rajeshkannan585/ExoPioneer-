// ==================================
// WeatherSystem.cs
// Handles dynamic weather events
// ==================================

using UnityEngine;
using System.Collections;

namespace ExoPioneer.Environment
{
    public enum WeatherType { Clear, AcidRain, FogStorm, MeteorShower, ColdStorm }

    public class WeatherSystem : MonoBehaviour
    {
        [Header("Weather Settings")]
        public WeatherType currentWeather = WeatherType.Clear;
        public float weatherDuration = 30f; // seconds
        public float timeBetweenWeathers = 60f;

        private float weatherTimer;

        void Start()
        {
            StartCoroutine(WeatherCycle());
        }

        IEnumerator WeatherCycle()
        {
            while (true)
            {
                // Random pick next weather
                currentWeather = (WeatherType)Random.Range(0, System.Enum.GetValues(typeof(WeatherType)).Length);
                Debug.Log("Weather changed: " + currentWeather);

                weatherTimer = 0f;
                while (weatherTimer < weatherDuration)
                {
                    weatherTimer += Time.deltaTime;
                    ApplyWeatherEffects();
                    yield return null;
                }

                currentWeather = WeatherType.Clear;
                Debug.Log("Weather cleared.");

                yield return new WaitForSeconds(timeBetweenWeathers);
            }
        }

        void ApplyWeatherEffects()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) return;

            var shield = player.GetComponent<ExoPioneer.PlayerSystem.ShieldSystem>();
            var health = player.GetComponent<ExoPioneer.PlayerSystem.PlayerHealth>();
            var survival = player.GetComponent<ExoPioneer.PlayerSystem.SurvivalSystem>();

            switch (currentWeather)
            {
                case WeatherType.AcidRain:
                    if (shield != null && shield.IsShieldActive())
                        shield.TakeShieldDamage(5f * Time.deltaTime);
                    else if (health != null)
                        health.TakeDamage(5f * Time.deltaTime);
                    break;

                case WeatherType.FogStorm:
                    // TODO: reduce player vision (post-processing / fog density)
                    break;

                case WeatherType.MeteorShower:
                    // TODO: spawn meteor prefabs randomly
                    break;

                case WeatherType.ColdStorm:
                    if (survival != null)
                        survival.UseStamina(2f * Time.deltaTime);
                    break;
            }
        }
    }
}
