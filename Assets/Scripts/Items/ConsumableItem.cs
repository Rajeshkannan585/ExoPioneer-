using UnityEngine;

public class ConsumableItem : MonoBehaviour
{
    public string itemName;
    public float healAmount;
    public float hungerRestore;
    public float oxygenRestore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var survival = other.GetComponent<PlayerSurvivalSystem>();
            if (survival)
            {
                survival.health = Mathf.Min(100f, survival.health + healAmount);
                survival.hunger = Mathf.Min(100f, survival.hunger + hungerRestore);
                survival.oxygen = Mathf.Min(100f, survival.oxygen + oxygenRestore);
            }

            Destroy(gameObject);
            Debug.Log($"🍖 Consumed: {itemName}");
        }
    }
}
