// ==================================
// CharacterSelector.cs
// Allows player to choose Male or Female character
// ==================================

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExoPioneer.PlayerSystem
{
    public class CharacterSelector : MonoBehaviour
    {
        [Header("Character Prefabs")]
        public GameObject malePrefab;
        public GameObject femalePrefab;

        public static string chosenGender = "Male"; // Default

        // Called from UI button (Main Menu)
        public void SelectMale()
        {
            chosenGender = "Male";
            Debug.Log("Selected Male Character");
        }

        public void SelectFemale()
        {
            chosenGender = "Female";
            Debug.Log("Selected Female Character");
        }

        // Load game scene with selected character
        public void StartGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        // Spawn the selected character in game scene
        void Awake()
        {
            if (SceneManager.GetActiveScene().name != "MainMenuScene")
            {
                GameObject prefab = (chosenGender == "Male") ? malePrefab : femalePrefab;
                if (prefab != null)
                {
                    Instantiate(prefab, Vector3.zero, Quaternion.identity);
                }
            }
        }
    }
}
