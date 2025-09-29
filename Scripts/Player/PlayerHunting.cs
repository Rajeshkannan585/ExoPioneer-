using UnityEngine;

namespace ExoPioneer.PlayerSystem
{
    public class PlayerHunting : MonoBehaviour
    {
        public float attackRange = 20f; // Maximum distance player can attack
        public int damage = 25; // Damage dealt per attack
        public Camera playerCamera; // Reference to player camera for raycast

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Left Click / Tap to attack
            {
                TryHunt();
            }
        }

        void TryHunt()
        {
            // Create a ray from the camera to the mouse position
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if ray hits any object within attack range
            if (Physics.Raycast(ray, out hit, attackRange))
            {
                // See if the object has AnimalHealth component
                AnimalHealth animal = hit.collider.GetComponent<AnimalHealth>();
                if (animal != null)
                {
                    animal.TakeDamage(damage); // Apply damage to animal
                    Debug.Log("Hit animal: " + hit.collider.name);
                }
            }
        }
    }
}
