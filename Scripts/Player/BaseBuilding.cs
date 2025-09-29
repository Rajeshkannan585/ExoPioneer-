// ==================================
// BaseBuilding.cs
// Allows player to place and build structures
// ==================================

using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class BaseBuilding : MonoBehaviour
    {
        public GameObject[] buildPrefabs;

        public void PlaceBuilding(int index, Vector3 position)
        {
            if (index < 0 || index >= buildPrefabs.Length) return;

            Instantiate(buildPrefabs[index], position, Quaternion.identity);
            Debug.Log("Placed building: " + buildPrefabs[index].name);
        }
    }
}
