using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;       // The weapon to give player
    public string pickupMessage = "Press E to pick up weapon";
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log(pickupMessage);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }
    }

    void PickupWeapon()
    {
        WeaponManager wm = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
        if (wm != null)
        {
            wm.AddWeapon(weaponPrefab);
            Debug.Log("🔫 Picked up weapon: " + weaponPrefab.name);
            Destroy(gameObject);
        }
    }
}
