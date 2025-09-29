// ==================================
// NetworkManagerCustom.cs
// Basic multiplayer setup using Mirror
// ==================================

using UnityEngine;
using Mirror; // Requires Mirror package from Unity Asset Store / GitHub

namespace ExoPioneer.Multiplayer
{
    public class NetworkManagerCustom : NetworkManager
    {
        [Header("Player Prefab")]
        public GameObject playerPrefabCustom;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            // Spawn player when they connect
            GameObject player = Instantiate(playerPrefabCustom, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player);

            Debug.Log("Player connected: " + conn.connectionId);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            Debug.Log("Player disconnected: " + conn.connectionId);
            base.OnServerDisconnect(conn);
        }
    }
}
