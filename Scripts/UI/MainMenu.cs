// ==================================
// MainMenu.cs
// Handles main menu buttons: New Game, Continue, Settings, Exit
// ==================================

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExoPioneer.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("References")]
        public ExoPioneer.Systems.SaveLoadManager saveManager;

        // Start a new game (load scene 1 for example)
        public void NewGame()
        {
            Debug.Log("Starting New Game...");
            SceneManager.LoadScene("GameScene"); // Replace with your main scene name
        }

        // Continue last save
        public void ContinueGame()
        {
            Debug.Log("Loading Save...");
            SceneManager.LoadScene("GameScene"); // After load, call saveManager.LoadGame()
        }

        // Open settings menu
        public void OpenSettings(GameObject settingsPanel)
        {
            settingsPanel.SetActive(true);
        }

        // Quit the game
        public void ExitGame()
        {
            Debug.Log("Exiting Game...");
            Application.Quit();
        }
    }
}
