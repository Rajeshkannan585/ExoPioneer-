using UnityEngine;

public class PortalToNextFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossTowerManager tower = FindObjectOfType<BossTowerManager>();
            if (tower != null)
            {
                tower.TeleportToNextFloor();
            }
        }
    }
}
