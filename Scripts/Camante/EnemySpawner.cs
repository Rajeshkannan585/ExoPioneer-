// ===============================
// EnemySpawner.cs
// Attach this script to an empty GameObject in Camante Camp.
// It will spawn enemies when the player enters the trigger zone.
// ===============================

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;        // What enemy to spawn
    public int enemyCount = 3;            // How many enemies spawn
    public float spawnRadius = 5f;        // How far apart they spawn
    public bool oneTimeSpawn = true;      // Spawn only once

    private bool _spawned = false;

    [Header("Spawn Effects")]
    public GameObject spawnEffect;        // Optional VFX when enemies appear
    public AudioClip spawnSound;          // Optional sound
    private AudioSource _audio;

    private void Start()
    {
        if (spawnSound != null)
        {
            _audio = gameObject.AddComponent<AudioSource>();
            _audio.clip = spawnSound;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_spawned && oneTimeSpawn) return;

        if (other.CompareTag("Player"))
        {
            _spawned = true;

            Debug.Log("EnemySpawner activated!");

            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
                pos.y = transform.position.y; // keep ground level
                Instantiate(enemyPrefab, pos, Quaternion.identity);
                
                if (spawnEffect) Instantiate(spawnEffect, pos, Quaternion.identity);
            }

            if (_audio) _audio.Play();
        }
    }
}
