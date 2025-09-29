// ===============================
// SpawnArea.cs
// This script defines an area in the game world where POIs (Points of Interest)
// can be spawned. Each SpawnArea covers a region and has a specific Biome type.
// ===============================

using UnityEngine;

namespace ExoPioneer.ProcGen
{
    [ExecuteAlways]  // Allows script to run in editor and play mode
    public class SpawnArea : MonoBehaviour
    {
        [Header("Bounds (World Units)")]
        public Vector2 size = new Vector2(200, 200); // The width and height of the area
        public Biome biome = Biome.Any;              // Which biome this area represents

        [Tooltip("Optional mask to avoid spawning in unwanted places (like water/lava).")]
        public LayerMask groundMask = ~0;            // Layers considered valid ground

        public float raycastHeight = 200f;           // Height from which raycast checks ground
        public float minSpacing = 10f;               // Minimum distance between POIs

        // Generate a random point inside this area (local to world position)
        public Vector3 RandomPoint(System.Random rng)
        {
            var half = size * 0.5f;
            var local = new Vector3(
                Mathf.Lerp(-half.x, half.x, (float)rng.NextDouble()),
                0f,
                Mathf.Lerp(-half.y, half.y, (float)rng.NextDouble())
            );
            return transform.TransformPoint(local);
        }

        // Draw gizmos in Unity Editor so we can see the area visually
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.7f, 1f, 0.15f);  // Light blue transparent
            var c = transform.position;
            var s = new Vector3(size.x, 0.1f, size.y);
            Gizmos.DrawCube(c, s);

            Gizmos.color = new Color(0f, 0.7f, 1f, 0.9f);   // Solid blue outline
            Gizmos.DrawWireCube(c, s);
        }
    }
}
