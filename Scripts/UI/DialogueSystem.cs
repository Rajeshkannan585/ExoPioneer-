// ==================================
// DialogueSystem.cs
// Handles NPC conversations with subtitles
// ==================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ExoPioneer.UI
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(2, 5)]
        public string text;
        public float duration = 3f; // seconds to show
    }

    public class DialogueSystem : MonoBehaviour
    {
        [Header("UI References")]
        public Text speakerText;
        public Text dialogueText;
        public GameObject dialoguePanel;

        private Coroutine dialogueRoutine;

        void Start()
        {
            dialoguePanel.SetActive(false);
        }

        public void StartDialogue(DialogueLine[] lines)
        {
            if (dialogueRoutine != null)
                StopCoroutine(dialogueRoutine);

            dialogueRoutine = StartCoroutine(ShowDialogue(lines));
        }

        IEnumerator ShowDialogue(DialogueLine[] lines)
        {
            dialoguePanel.SetActive(true);

            foreach (var line in lines)
            {
                speakerText.text = line.speakerName;
                dialogueText.text = line.text;

                yield return new WaitForSeconds(line.duration);
            }

            dialoguePanel.SetActive(false);
        }
    }
}
