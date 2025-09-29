// ===============================
// RandomPlaceGenerator.cs
// This script is the core system that places POIs (Points of Interest)
// randomly in the game world, based on rules defined in POIDefinition
// and SpawnArea. It ensures variety, spacing, and deterministic results
// if the same seed is used.
// ===============================

using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ExoPioneer.ProcGen
{
    public class RandomPlaceGenerator : MonoBehaviour
    {
        [Header("Content")]
        public List<POIDefinition> poiDefinitions = new(); // All available POIs
        [Tooltip("Areas where POIs can spawn. Place multiple for different biomes/regions.")]
        public List<SpawnArea> spawnAreas = new();

        [Header("Randomness")]
        public int seed = 12345;               // Seed for reproducible results
        public bool useSystemTimeSeed = false; // If true, uses real-time seed (more random)

        [Header("Difficulty Scaling")]
        [Range(0f,1f)] public float gameProgress = 0f; // 0 = start, 1 = end
        public AnimationCurve spawnBudgetByDifficulty = AnimationCurve.Linear(0, 12, 1, 24);

        [Header("Placement")]
        public float minDistanceBetweenPOIs = 15f; // Minimum spacing
        public LayerMask groundMask = ~0;          // Must hit ground
        public float heightSample = 300f;          // Height for raycast

        [Header("Debug/Gizmos")]
        public bool clearOnGenerate = true; // Clear old POIs when regenerating
        public bool drawGizmos = true;      // Draw debug spheres for spawned POIs

        private readonly List<Transform> _spawned = new(); // Track spawned POIs
        private System.Random _rng;

        // ===============================
        // Generate all POIs based on rules
        // ===============================
        public void Generate()
        {
            if (useSystemTimeSeed) seed = System.Environment.TickCount;
            _rng = new System.Random(seed);

            // Clear old POIs
            if (clearOnGenerate)
            {
                foreach (var t in _spawned)
                    if (t) DestroyImmediate(t.gameObject);
                _spawned.Clear();
            }

            // Determine budget from difficulty curve
            var budget = Mathf.RoundToInt(spawnBudgetByDifficulty.Evaluate(gameProgress));

            // Track counts
            var pool = new List<POIDefinition>(poiDefinitions);
            var counts = new Dictionary<POIDefinition, int>();
            foreach (var def in pool) counts[def] = 0;

            int safety = 10000; // Prevent infinite loops
            while (budget > 0 && safety-- > 0)
            {
                var def = WeightedPick(pool);
                if (def == null) break;

                if (counts[def] >= def.maxPerMap) { pool.Remove(def); continue; }
                if (_rng.NextDouble() > def.spawnChance) { continue; }

                var area = PickAreaFor(def);
                if (area == null) { continue; }

                if (TryPlace(def, area, out var placed))
                {
                    counts[def]++;
                    budget--;
                    _spawned.Add(placed);
                }
            }

            // Ensure minimum count
            foreach (var def in poiDefinitions)
            {
                while (counts[def] < def.minPerMap)
                {
                    var area = PickAreaFor(def);
                    if (area == null) break;
                    if (!TryPlace(def, area, out var placed)) break;
                    counts[def]++;
                    _spawned.Add(placed);
                }
            }
        }

        // Weighted random pick (rarity & progress affect chance)
        private POIDefinition WeightedPick(List<POIDefinition> pool)
        {
            if (pool.Count == 0) return null;
            float sum = 0f;
            foreach (var d in pool)
            {
                var rarityWeight = 1f / Mathf.Max(1, d.rarity);
                var prog = Mathf.Max(0.01f, d.difficultyWeightByProgress.Evaluate(gameProgress));
                sum += rarityWeight * prog;
            }
            var r = (float)_rng.NextDouble() * sum;
            foreach (var d in pool)
            {
                var rarityWeight = 1f / Mathf.Max(1, d.rarity);
                var prog = Mathf.Max(0.01f, d.difficultyWeightByProgress.Evaluate(gameProgress));
                r -= rarityWeight * prog;
                if (r <= 0f) return d;
            }
            return pool[pool.Count-1];
        }

        // Pick an area matching POI's allowed biomes
        private SpawnArea PickAreaFor(POIDefinition def)
        {
            var candidates = new List<SpawnArea>();
            foreach (var a in spawnAreas)
            {
                if (a == null) continue;
                if (def.allowedBiomes.Length == 0) { candidates.Add(a); continue; }
                foreach (var b in def.allowedBiomes)
                {
                    if (b == Biome.Any || b == a.biome) { candidates.Add(a); break; }
                }
            }
            if (candidates.Count == 0) return null;
            return candidates[_rng.Next(candidates.Count)];
        }

        // Try to place a POI prefab at a random valid location
        private bool TryPlace(POIDefinition def, SpawnArea area, out Transform placed)
        {
            placed = null;
            const int attempts = 30;
            for (int i = 0; i < attempts; i++)
            {
                var point = area.RandomPoint(_rng) + Vector3.up * heightSample;
                if (!Physics.Raycast(point, Vector3.down, out var hit, heightSample * 2f, groundMask))
                    continue;

                var pos = hit.point;

                // Check spacing
                bool tooClose = false;
                foreach (var t in _spawned)
                {
                    if (!t) continue;
                    if (Vector3.Distance(t.position, pos) < Mathf.Max(minDistanceBetweenPOIs, area.minSpacing))
                    { tooClose = true; break; }
                }
                if (tooClose) continue;

                // Instantiate prefab
                if (def.prefabs == null || def.prefabs.Length == 0) return false;
                var prefab = def.prefabs[_rng.Next(def.prefabs.Length)];
                var rot = Quaternion.Euler(0f, (float)_rng.NextDouble() * 360f, 0f);
                var go = Instantiate(prefab, pos, rot, transform);
                go.name = $"POI_{def.displayName}";

                // Optional marker
                if (def.markerPrefab)
                {
                    var mk = Instantiate(def.markerPrefab, pos + Vector3.up * 2f, Quaternion.identity, go.transform);
                    mk.name = "Marker";
                }

                placed = go.transform;
                return true;
            }
            return false;
        }

#if UNITY_EDITOR
        // Custom editor buttons
        [CustomEditor(typeof(RandomPlaceGenerator))]
        private class RandomPlaceGeneratorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                var gen = (RandomPlaceGenerator)target;
                if (GUILayout.Button("Generate Now")) { gen.Generate(); }
                if (GUILayout.Button("Clear Spawned"))
                {
                    foreach (var t in gen._spawned) if (t) DestroyImmediate(t.gameObject);
                    gen._spawned.Clear();
                }
            }
        }
#endif

        // Draw gizmos for spawned POIs
        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;
            Gizmos.color = new Color(1f, 1f, 0f, 0.25f);
            foreach (var t in _spawned)
            {
                if (!t) continue;
                Gizmos.DrawSphere(t.position + Vector3.up * 0.5f, 1.5f);
            }
        }
    }
}
