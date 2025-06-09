using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieType
{
    public GameObject prefab;
    [Range(0, 100)]
    public int spawnChance; // percentage chance (50/40/10 for female/male/monster)
}

public class ZombieSpawner : MonoBehaviour
{
    [Header("Time Control Reference")]
    [SerializeField] private TimeController timeController;

    [Header("Zombie Settings")]
    [SerializeField] private List<ZombieType> zombieTypes;
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
        if (timeController == null || player == null || zombieTypes == null || zombieTypes.Count == 0)
            return;

        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        TimeSpan nightStart = TimeSpan.FromHours(19); // 7 PM
        TimeSpan nightEnd = TimeSpan.FromHours(timeController.sunriseHour);

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
                GameObject prefab = GetRandomZombiePrefab();
                GameObject zombie = Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedZombies.Add(zombie);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetRandomZombiePrefab()
    {
        int roll = UnityEngine.Random.Range(0, 100);
        int cumulative = 0;

        foreach (var type in zombieTypes)
        {
            cumulative += type.spawnChance;
            if (roll < cumulative)
                return type.prefab;
        }

        return zombieTypes[0].prefab;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = player.position + new Vector3(randomDirection.x, 0, randomDirection.y) * distance;

        if (Physics.Raycast(spawnPosition + Vector3.up * 50, Vector3.down, out RaycastHit hit, 100f))
        {
            spawnPosition.y = hit.point.y;
        }

        return spawnPosition;
    }
}