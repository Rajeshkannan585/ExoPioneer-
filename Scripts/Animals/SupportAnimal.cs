// ==============================
// SupportAnimal.cs
// Base script for animals that can follow & protect the player
// ==============================

using UnityEngine;
using UnityEngine.AI;

namespace ExoPioneer.Animals
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SupportAnimal : MonoBehaviour
    {
        public string animalName = "Guardian Beast"; // Display name
        public float health = 100f;
        public float damage = 10f;
        public float followDistance = 3f;

        private Transform player;
        private NavMeshAgent agent;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (player == null) return;

            float distance = Vector3.Distance(transform.position, player.position);

            // Follow the player if too far
            if (distance > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.ResetPath();
            }
        }

        public void Attack(GameObject target)
        {
            // Simple attack function
            Debug.Log(animalName + " attacks " + target.name + " for " + damage + " damage!");
        }
    }
}
