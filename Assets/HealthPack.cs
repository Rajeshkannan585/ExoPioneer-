using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20; // எவ்வளவு heal பண்ணணும்

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.HealPlayer(healAmount);
            }

            Debug.Log("Player picked up health pack!");
            Destroy(gameObject); // Use ஆன பிறகு pack disappear ஆகும்
        }
    }
}
