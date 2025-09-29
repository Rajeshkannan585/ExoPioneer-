// ==================================
// EmoteSystem.cs
// Allows players to trigger emote animations in multiplayer
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Multiplayer
{
    public class EmoteSystem : NetworkBehaviour
    {
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        [Command]
        public void CmdPlayEmote(string emoteName)
        {
            RpcPlayEmote(emoteName);
        }

        [ClientRpc]
        void RpcPlayEmote(string emoteName)
        {
            if (animator != null)
            {
                animator.SetTrigger(emoteName);
            }
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            // Example: Press keys for emotes
            if (Input.GetKeyDown(KeyCode.Alpha1)) CmdPlayEmote("Wave");
            if (Input.GetKeyDown(KeyCode.Alpha2)) CmdPlayEmote("Dance");
            if (Input.GetKeyDown(KeyCode.Alpha3)) CmdPlayEmote("Laugh");
        }
    }
}
