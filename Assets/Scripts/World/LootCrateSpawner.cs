using UnityEngine;
using System.Collections;

public class LootCrateSpawner : MonoBehaviour
{
    public GameObject lootCratePrefab;  // Crate prefab
    public float spawnInterval = 60f;   // Time between drops
    public float dropHeight = 50f;      // Drop altitude
    public Vector3 areaSize = new Vector3(100, 0, 100); // Drop zone size

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCrate();
        }
    }

    void SpawnCrate()
    {
        Vector3 randomPos = new Vector3(
            transform.position.x + Random.Range(-areaSize.x / 2, areaSize.x / 2),
            transform.position.y + dropHeight,
            transform.position.z + Random.Range(-areaSize.z / 2, areaSize.z / 2)
        );

        GameObject crate = Instantiate(lootCratePrefab, randomPos, Quaternion.identity);
        Rigidbody rb = crate.GetComponent<Rigidbody>();
        if (rb != null) rb.useGravity = true;

        Debug.Log("🪂 Loot Crate Dropped at " + randomPos);
    }
}
