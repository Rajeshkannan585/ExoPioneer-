// ===============================
// SpawnArea.cs
// Defines a rectangular region in the world where POIs can spawn.
// Handles biome type, spacing, and placement gizmos.
// ===============================

using UnityEngine;

namespace ExoPioneer.ProcGen
{
    [ExecuteAlways]
    public class SpawnArea : MonoBehaviour
    {
        [Header("Bounds (World Units)")]
        public Vector2 size = new Vector2(200, 200);
        public Biome biome = Biome.Any;

        [Tooltip("Optional mask to avoid water/lava/etc. Provide a LayerMask where placement is allowed.")]
        public LayerMask groundMask = ~0;

        public float raycastHeight = 200f;
        public float minSpacing = 10f; // prevent overlaps

        // Generate a random point inside this area
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

        // Draw visualization in editor
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 0.7f, 1f, 0.15f);
            var c = transform.position;
            var s = new Vector3(size.x, 0.1f, size.y);
            Gizmos.DrawCube(c, s);

            Gizmos.color = new Color(0f, 0.7f, 1f, 0.9f);
            Gizmos.DrawWireCube(c, s);
        }
    }
}
