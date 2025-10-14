using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class BossTowerManager : MonoBehaviour
{
    [System.Serializable]
    public class TowerFloor
    {
        public string floorName;
        public GameObject[] enemyPrefabs;
        public Transform[] spawnPoints;
        public AudioClip floorMusic;
        public GameObject bossPrefab;
        public float rewardCoins;
    }

    [Header("Tower Setup")]
    public List<TowerFloor> towerFloors = new List<TowerFloor>();
    public int currentFloor = 0;

    [Header("Environment FX")]
    public GameObject teleportFX;
    public Transform playerSpawnPoint;
    public AudioSource musicSource;

    [Header("UI Elements")]
    public CanvasGroup floorIntroPanel;
    public TMPro.TextMeshProUGUI floorText;

    [Header("Rewards")]
    public GameObject lootChestPrefab;
    public GameObject exitPortalPrefab;

    private GameObject currentBoss;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        LoadFloor(currentFloor);
    }

    void LoadFloor(int index)
    {
        if (index >= towerFloors.Count)
        {
            Debug.Log("🏆 All floors completed!");
            SceneManager.LoadScene("VictoryScene");
            return;
        }

        TowerFloor floor = towerFloors[index];
        StartCoroutine(FloorIntro(floor.floorName));

        // Play floor music
        if (floor.floorMusic != null)
        {
            musicSource.clip = floor.floorMusic;
            musicSource.Play();
        }

        // Spawn enemies
        foreach (var point in floor.spawnPoints)
        {
            GameObject enemy = Instantiate(floor.enemyPrefabs[Random.Range(0, floor.enemyPrefabs.Length)], point.position, point.rotation);
            spawnedEnemies.Add(enemy);
        }

        // Spawn boss if any
        if (floor.bossPrefab != null)
        {
            currentBoss = Instantiate(floor.bossPrefab, floor.spawnPoints[0].position + Vector3.up * 3, Quaternion.identity);
        }

        if (playerSpawnPoint != null)
            GameObject.FindWithTag("Player").transform.position = playerSpawnPoint.position;
    }

    void Update()
    {
        // Check if all enemies are cleared
        spawnedEnemies.RemoveAll(e => e == null);

        if (currentBoss == null && spawnedEnemies.Count == 0)
        {
            CompleteFloor();
        }
    }

    void CompleteFloor()
    {
        TowerFloor floor = towerFloors[currentFloor];
        Debug.Log("✅ Floor Cleared: " + floor.floorName);

        // Reward coins
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins", 0) + (int)floor.rewardCoins);

        // Spawn loot
        if (lootChestPrefab != null)
        {
            Instantiate(lootChestPrefab, playerSpawnPoint.position + Vector3.forward * 2, Quaternion.identity);
        }

        // Create portal to next floor
        if (exitPortalPrefab != null)
        {
            Instantiate(exitPortalPrefab, playerSpawnPoint.position + Vector3.right * 3, Quaternion.identity);
        }

        currentFloor++;
    }

    IEnumerator FloorIntro(string floorName)
    {
        floorText.text = "Entering " + floorName;
        floorIntroPanel.alpha = 1;
        yield return new WaitForSeconds(3f);
        floorIntroPanel.alpha = 0;
    }

    public void TeleportToNextFloor()
    {
        if (teleportFX != null)
            Instantiate(teleportFX, playerSpawnPoint.position, Quaternion.identity);

        foreach (var e in spawnedEnemies)
            if (e) Destroy(e);

        LoadFloor(currentFloor);
    }
}
