// ==================================
// Projectile.cs
// Handles projectile movement and damage
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Weapons
{
    public class Projectile : NetworkBehaviour
    {
        public float speed = 50f;
        public int damage = 25;
        public float lifeTime = 3f;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        [ServerCallback]
        void OnTriggerEnter(Collider other)
        {
            // Apply damage if target has health
            var playerHealth = other.GetComponent<ExoPioneer.PlayerSystem.PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            var animalHealth = other.GetComponent<ExoPioneer.Animals.AnimalHealth>();
            if (animalHealth != null)
            {
                animalHealth.TakeDamage(damage);
            }

            NetworkServer.Destroy(gameObject);
        }
    }
}
