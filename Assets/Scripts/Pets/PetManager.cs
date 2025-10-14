using UnityEngine;
using System.Collections.Generic;

public class PetManager : MonoBehaviour
{
    public List<GameObject> availablePets;  // List of all pet prefabs
    public Transform summonPoint;           // Where pet spawns near player
    private GameObject activePet;           // Current summoned pet

    public void SummonPet(int petIndex)
    {
        if (petIndex < 0 || petIndex >= availablePets.Count)
        {
            Debug.LogWarning("Invalid pet index!");
            return;
        }

        // Remove old pet if any
        if (activePet != null)
            Destroy(activePet);

        // Spawn new pet
        activePet = Instantiate(availablePets[petIndex], summonPoint.position, Quaternion.identity);
        activePet.GetComponent<PetController>().player = transform;

        Debug.Log("Summoned Pet: " + availablePets[petIndex].name);
    }

    public void DespawnPet()
    {
        if (activePet != null)
        {
            Destroy(activePet);
            Debug.Log("Pet despawned");
        }
    }
}
