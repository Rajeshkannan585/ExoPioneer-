// ==================================
// PetEvolution.cs
// Handles pet leveling, evolution, and ability unlocks
// ==================================

using UnityEngine;

namespace ExoPioneer.Animals
{
    [System.Serializable]
    public class PetEvolutionStage
    {
        public string stageName;     // Example: "Baby Bot"
        public GameObject modelPrefab;
        public int xpRequired;
        public string abilityUnlocked;
    }

    public class PetEvolution : MonoBehaviour
    {
        [Header("Pet Settings")]
        public string petName = "Unnamed Pet";
        public int currentXP = 0;
        public int currentStageIndex = 0;
        public PetEvolutionStage[] stages;

        private GameObject currentModel;

        void Start()
        {
            if (stages.Length > 0)
                SetStage(0);
        }

        public void AddXP(int amount)
        {
            currentXP += amount;
            Debug.Log(petName + " gained " + amount + " XP!");

            // Check if ready to evolve
            if (currentStageIndex < stages.Length - 1 &&
                currentXP >= stages[currentStageIndex + 1].xpRequired)
            {
                Evolve();
            }
        }

        void Evolve()
        {
            currentStageIndex++;
            SetStage(currentStageIndex);

            Debug.Log(petName + " evolved into " + stages[currentStageIndex].stageName +
                      " and unlocked: " + stages[currentStageIndex].abilityUnlocked);
        }

        void SetStage(int index)
        {
            if (currentModel != null)
                Destroy(currentModel);

            currentModel = Instantiate(stages[index].modelPrefab, transform);
        }
    }
}
