// ===============================
// POIDefinition.cs
// This script defines all the properties of a Point of Interest (POI).
// Each POI (like Crystal Cave, Alien Hive, or Camante Camp) will be created
// as a ScriptableObject using this definition.
// ===============================

using UnityEngine;

namespace ExoPioneer.ProcGen
{
    // ScriptableObject lets us create POI data assets in the Unity Editor.
    [CreateAssetMenu(menuName:"ExoPioneer/ProcGen/POI Definition", fileName:"POI_YourName")] 
    public class POIDefinition : ScriptableObject
    {
        [Header("Basic Info")]
        public string displayName = "Unnamed POI";   // Example: "Crystal Cave" or "Camante Camp"
        public POICategory category = POICategory.Exploration; // Which category this POI belongs to

        [Header("Spawn Rules")]
        [Tooltip("Which biomes can this POI appear in? Use 'Any' to allow everywhere.")]
        public Biome[] allowedBiomes = new[] { Biome.Any };

        [Range(0f, 1f)] public float spawnChance = 0.5f; // Probability (0 = never, 1 = always)
        [Min(0)] public int minPerMap = 0;               // Minimum times it must spawn per map
        [Min(0)] public int maxPerMap = 3;               // Maximum times it can spawn per map

        [Tooltip("Higher rarity means fewer spawns. 1 = common, 5 = legendary")]
        [Range(1,5)] public int rarity = 2;

        [Header("Prefabs & VFX")]
        public GameObject[] prefabs;    // Prefabs to spawn for this POI (one will be picked randomly)
        public GameObject markerPrefab; // Optional map marker (UI icon / minimap marker)

        [Header("Gameplay Hooks")]
        public bool requiresShield;     // Does this area require shield (radiation/acid)?
        public bool isBossArea;         // Is this a boss arena?
        public AnimationCurve difficultyWeightByProgress = AnimationCurve.Linear(0,1,1,1);

        [Header("Camante Special (Settlement Camp)")]
        public bool hasLootableHuts;       // Does the camp have lootable huts?
        public bool hasAlienShrine;        // Does it contain an alien shrine?
        public bool enemyAmbushPossible;   // Can enemies ambush here?
        public bool hasTradeNPC;           // Does it have a trade NPC?
    }
}
