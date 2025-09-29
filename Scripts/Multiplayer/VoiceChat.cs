// ==================================
// VoiceChat.cs
// Handles microphone input and transmits to other players
// ==================================

using UnityEngine;
using Mirror;

namespace ExoPioneer.Multiplayer
{
    [RequireComponent(typeof(AudioSource))]
    public class VoiceChat : NetworkBehaviour
    {
        private AudioSource audioSource;
        private AudioClip micClip;

        [Header("Voice Settings")]
        public int sampleRate = 16000;
        public string micDevice;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if (isLocalPlayer)
            {
                if (Microphone.devices.Length > 0)
                {
                    micDevice = Microphone.devices[0];
                    micClip = Microphone.Start(micDevice, true, 1, sampleRate);
                }
            }
        }

        void Update()
        {
            if (!isLocalPlayer) return;

            if (Input.GetKey(KeyCode.V)) // Hold V to talk
            {
                float[] samples = new float[micClip.samples * micClip.channels];
                micClip.GetData(samples, 0);
                CmdSendVoice(samples);
            }
        }

        [Command]
        void CmdSendVoice(float[] samples)
        {
            RpcPlayVoice(samples);
        }

        [ClientRpc]
        void RpcPlayVoice(float[] samples)
        {
            if (isLocalPlayer) return; // Don't playback self

            AudioClip clip = AudioClip.Create("Voice", samples.Length, 1, sampleRate, false);
            clip.SetData(samples, 0);
            audioSource.PlayOneShot(clip);
        }
    }
}
