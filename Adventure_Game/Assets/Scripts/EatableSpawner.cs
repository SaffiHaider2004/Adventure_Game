using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EatableSpawner : MonoBehaviour
{
    public GameObject healthEatablePrefab;
    public GameObject staminaEatablePrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 30f;

    void Start()
    {
        StartCoroutine(SpawnEatableRoutine());
    }

    IEnumerator SpawnEatableRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEatable();
        }
    }

    void SpawnEatable()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 0)
        {
            Debug.LogWarning("No players found with tag 'Player'.");
            return;
        }

        // Choose a random player
        GameObject randomPlayer = players[Random.Range(0, players.Length)];

        // Choose eatable type (50% health, 50% stamina)
        GameObject prefabToSpawn = Random.value < 0.5f ? healthEatablePrefab : staminaEatablePrefab;

        // Random position near the player
        Vector3 spawnPosition = randomPlayer.transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = randomPlayer.transform.position.y;

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
