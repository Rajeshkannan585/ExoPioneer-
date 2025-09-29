// ==================================
// CutsceneTrigger.cs
// Triggers cutscenes (memories/flashbacks)
// when player enters area or completes quest
// ==================================

using UnityEngine;

namespace ExoPioneer.Story
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [Header("Cutscene Settings")]
        public string cutsceneName = "KallaraiMemory01";
        public GameObject cutscenePrefab; // Optional prefab with animations/UI
        public bool triggerOnce = true;

        private bool hasTriggered = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && (!hasTriggered || !triggerOnce))
            {
                PlayCutscene();
                hasTriggered = true;
            }
        }

        void PlayCutscene()
        {
            Debug.Log("Cutscene Triggered: " + cutsceneName);

            if (cutscenePrefab != null)
            {
                Instantiate(cutscenePrefab);
            }

            // TODO: Lock player controls, play cinematic camera, dialogue, audio
        }
    }
}
