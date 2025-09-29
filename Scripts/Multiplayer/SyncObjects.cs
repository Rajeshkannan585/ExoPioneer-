// ==================================
// SyncObjects.cs
// Synchronizes vehicles, pets, and building placement across multiplayer
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Multiplayer
{
    public class SyncObjects : NetworkBehaviour
    {
        [SyncVar] public Vector3 syncedPosition;
        [SyncVar] public Quaternion syncedRotation;

        [Header("Settings")]
        public float lerpRate = 10f;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (isLocalPlayer || hasAuthority)
            {
                CmdSyncTransform(transform.position, transform.rotation);
            }
            else
            {
                // Smoothly interpolate remote objects
                transform.position = Vector3.Lerp(transform.position, syncedPosition, Time.deltaTime * lerpRate);
                transform.rotation = Quaternion.Lerp(transform.rotation, syncedRotation, Time.deltaTime * lerpRate);
            }
        }

        [Command]
        void CmdSyncTransform(Vector3 pos, Quaternion rot)
        {
            syncedPosition = pos;
            syncedRotation = rot;
        }

        // Example: Spawn a building (called by Player)
        [Command]
        public void CmdPlaceBuilding(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            GameObject building = Instantiate(prefab, pos, rot);
            NetworkServer.Spawn(building);
        }

        // Example: Spawn a pet (called by Player)
        [Command]
        public void CmdSpawnPet(GameObject petPrefab, Vector3 pos)
        {
            GameObject pet = Instantiate(petPrefab, pos, Quaternion.identity);
            NetworkServer.Spawn(pet, connectionToClient); // Assign ownership
        }
    }
}
