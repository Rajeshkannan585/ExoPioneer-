
// ===============================
// Lootable.cs
// Attach this script to lootable huts, crates, or chests in Camante.
// When the player interacts, they receive items/resources.
// ===============================

using UnityEngine;

public class Lootable : MonoBehaviour
{
    [Header("Loot Settings")]
    public string lootName = "Alien Supply Crate";
    public int minAmount = 1;
    public int maxAmount = 5;
    public AudioClip lootSound;

    private bool _looted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_looted) return;

        if (other.CompareTag("Player"))
        {
            int amount = Random.Range(minAmount, maxAmount + 1);
            Debug.Log($"Player looted {amount} items from {lootName}");

            if (lootSound)
            {
                AudioSource.PlayClipAtPoint(lootSound, transform.position);
            }

            _looted = true;
            // TODO: Add inventory system integration here
            Destroy(gameObject, 0.5f); // remove the crate after looting
        }
    }
}
