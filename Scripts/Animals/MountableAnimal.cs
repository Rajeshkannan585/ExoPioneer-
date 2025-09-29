// ==============================
// MountableAnimal.cs
// Animal that the player can mount and ride like a vehicle
// ==============================

using UnityEngine;

namespace ExoPioneer.Animals
{
    public class MountableAnimal : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float turnSpeed = 80f;

        private bool isMounted = false;
        private Transform rider;

        void Update()
        {
            if (isMounted && rider != null)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                // Movement
                Vector3 move = transform.forward * v * moveSpeed * Time.deltaTime;
                transform.position += move;

                // Turning
                transform.Rotate(Vector3.up, h * turnSpeed * Time.deltaTime);

                // Rider follow mount
                rider.position = transform.position + Vector3.up * 1.5f;
            }
        }

        public void Mount(Transform player)
        {
            rider = player;
            isMounted = true;

            // Hide player model or disable controller
            player.gameObject.SetActive(false);
        }

        public void Dismount(Vector3 dismountPos)
        {
            if (rider != null)
            {
                rider.position = dismountPos;
                rider.gameObject.SetActive(true);
            }

            rider = null;
            isMounted = false;
        }
    }
}
