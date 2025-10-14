using UnityEngine;

public class AnimalMountTrigger : MonoBehaviour
{
    public AnimalFlyingVehicle flyingAnimal;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            flyingAnimal.MountPlayer(other.gameObject);
        }
    }
}
