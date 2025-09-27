using UnityEngine;

public class OxygenTank : MonoBehaviour
{
    public float oxygenAmount = 25f; // How much oxygen this tank gives

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find GameManager and add oxygen
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.AddOxygen(oxygenAmount);
            }

            // Destroy the tank after pickup
            Destroy(gameObject);
        }
    }
}
