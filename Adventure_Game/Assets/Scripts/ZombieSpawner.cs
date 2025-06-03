using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Time Control Reference")]
    [SerializeField] private TimeController timeController;

    [Header("Zombie Settings")]
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxZombiesAtOnce = 10;
    [SerializeField] private float minSpawnDistance = 15f;
    [SerializeField] private float maxSpawnDistance = 30f;

    [Header("Player Reference")]
    [SerializeField] private Transform player;

    private List<GameObject> spawnedZombies = new List<GameObject>();
    private bool isNight = false;
    private Coroutine spawnCoroutine;

    void Update()
    {
        if (timeController == null || player == null || zombiePrefab == null)
            return;

        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        TimeSpan nightStart = TimeSpan.FromHours(19); // 7 PM
        TimeSpan nightEnd = TimeSpan.FromHours(timeController.sunriseHour); // Morning

        bool currentlyNight = currentTime >= nightStart || currentTime < nightEnd;

        if (currentlyNight && !isNight)
        {
            isNight = true;
            spawnCoroutine = StartCoroutine(SpawnZombies());
        }
        else if (!currentlyNight && isNight)
        {
            isNight = false;
            if (spawnCoroutine != null)
                StopCoroutine(spawnCoroutine);

            // Optionally destroy all zombies in the morning
            foreach (var zombie in spawnedZombies)
            {
                if (zombie != null)
                    Destroy(zombie);
            }
            spawnedZombies.Clear();
        }
    }

    private IEnumerator SpawnZombies()
    {
        while (isNight)
        {
            if (spawnedZombies.Count < maxZombiesAtOnce)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
                spawnedZombies.Add(zombie);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, 0, randomDirection.y) * distance;

        // Optionally, adjust height using raycast (ground detection)
        if (Physics.Raycast(spawnPosition + Vector3.up * 50, Vector3.down, out RaycastHit hit, 100f))
        {
            spawnPosition.y = hit.point.y;
        }

        return spawnPosition;
    }
}
