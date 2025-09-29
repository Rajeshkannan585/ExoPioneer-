// ==================================
// VehicleController.cs
// Basic vehicle driving system for cars/hovercrafts
// ==================================

using UnityEngine;

namespace ExoPioneer.Vehicles
{
    [RequireComponent(typeof(Rigidbody))]
    public class VehicleController : MonoBehaviour
    {
        [Header("Vehicle Settings")]
        public float moveSpeed = 20f;    // Forward/Backward speed
        public float turnSpeed = 60f;    // Turning speed
        public float brakeForce = 50f;   // Braking power

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Get input (W/S for forward/back, A/D for turn)
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");

            // Move forward/backward
            Vector3 move = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            // Turn vehicle
            Quaternion turn = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turn);

            // Brake (Space key)
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity *= (1f - brakeForce * Time.fixedDeltaTime);
            }
        }
    }
}
