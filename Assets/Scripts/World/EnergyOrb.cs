using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    public float energyValue = 25f;

    void OnTriggerEnter(Collider other)
    {
        FlyingAnimalCombat combat = other.GetComponent<FlyingAnimalCombat>();
        if (combat != null)
        {
            combat.RechargeEnergy(energyValue);
            Debug.Log("⚡ Energy orb collected!");
            Destroy(gameObject);
        }
    }
}
