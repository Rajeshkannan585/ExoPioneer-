using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.HealPlayer(healAmount);
            }

            Debug.Log("Health Pack Collected! +" + healAmount);
            Destroy(gameObject); // once collected, remove health pack
        }
    }
}
