// ===============================
// POIDefinition.cs
// ScriptableObject to define a Point of Interest (POI)
// Examples: Camante Camp, Alien Shrine, Resource Crate, Danger Zone
// ===============================

using UnityEngine;

namespace ExoPioneer.ProcGen
{
    [CreateAssetMenu(menuName: "ExoPioneer/ProcGen/POI Definition", fileName: "POI_YourName")]
    public class POIDefinition : ScriptableObject
    {
        [Header("Basic Info")]
        public string displayName = "Unnamed POI";       // e.g., "Crystal Cave", "Camante Camp"
        public POICategory category = POICategory.Exploration;

        [Tooltip("Preferred biomes for this POI. Use 'Any' to allow everywhere")]
        public Biome[] allowedBiomes = new[] { Biome.Any };

        [Range(0f, 1f)]
        public float spawnChance = 0.5f;        // probability when rolled

        [Min(0)]
        public int minPerMap = 0;               // minimum count per map
        [Min(0)]
        public int maxPerMap = 3;               // maximum count per map

        [Tooltip("Higher rarity means fewer spawns. 1=common, 5=legendary")]
        [Range(1, 5)]
        public int rarity = 2;

        [Header("Prefabs & VFX")]
        public GameObject[] prefabs;            // pick 1 randomly
        public GameObject markerPrefab;         // optional map marker

        [Header("Gameplay Hooks")]
        public bool requiresShield;             // e.g., radiation/acid zones
        public bool isBossArea;                 // boss fight zone
        public AnimationCurve difficultyWeightByProgress = AnimationCurve.Linear(0, 1, 1, 1);

        [Header("Camante Special Settings")]
        public bool hasLootableHuts;
        public bool hasAlienShrine;
        public bool enemyAmbushPossible;
        public bool hasTradeNPC;
    }
}
