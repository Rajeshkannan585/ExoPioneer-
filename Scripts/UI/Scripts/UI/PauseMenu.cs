// ==================================
// PauseMenu.cs
// Handles in-game pause menu
// ==================================

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExoPioneer.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("References")]
        public GameObject pausePanel; // UI Panel for pause menu
        public ExoPioneer.Systems.SaveLoadManager saveManager;
        public ExoPioneer.PlayerSystem.PlayerHealth playerHealth;
        public ExoPioneer.PlayerSystem.SurvivalSystem survival;
        public ExoPioneer.PlayerSystem.InventorySystem inventory;

        private bool isPaused = false;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) // Press ESC to toggle pause
            {
                if (isPaused) Resume();
                else Pause();
            }
        }

        public void Pause()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // Freeze game time
            isPaused = true;
        }

        public void Resume()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f; // Resume game time
            isPaused = false;
        }

        public void SaveGame()
        {
            if (saveManager != null)
                saveManager.SaveGame(playerHealth, survival, inventory);
        }

        public void LoadGame()
        {
            if (saveManager != null)
                saveManager.LoadGame(playerHealth, survival, inventory);
        }

        public void ExitToMainMenu()
        {
            Time.timeScale = 1f; // Reset time
            SceneManager.LoadScene("MainMenuScene"); // Replace with your menu scene name
        }
    }
}
