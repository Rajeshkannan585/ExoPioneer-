// ===============================
// Biome.cs
// This script defines different biome types that exist in the game world.
// A biome is an environmental region such as a forest, desert, or swamp.
// Points of Interest (POIs) can be restricted to spawn only in certain biomes.
// ===============================

using UnityEngine;

namespace ExoPioneer.ProcGen
{
    // Enum = a list of possible values for Biomes.
    public enum Biome
    {
        Any,       // Can spawn anywhere, no biome restrictions.
        Forest,    // Dense trees, jungle-like areas.
        Desert,    // Dry, sandy regions with little vegetation.
        Swamp,     // Wet, marshy areas with water and mud.
        Volcano,   // Lava, ash, and dangerous volcanic terrain.
        Tundra,    // Frozen ice, snow, and cold regions.
        Ruins      // Abandoned or ancient structures, broken cities.
    }
}
