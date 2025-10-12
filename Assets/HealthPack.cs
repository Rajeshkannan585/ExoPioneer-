using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.HealPlayer(healAmount);
            }

            Debug.Log("Player picked up a health pack!");
            Destroy(gameObject);
        }
    }
}
