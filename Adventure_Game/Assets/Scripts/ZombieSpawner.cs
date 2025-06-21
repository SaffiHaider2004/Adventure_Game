using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieType
{
    public GameObject prefab;
    [Range(0, 100)]
    public int spawnChance;
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
    private WaitForSeconds wait;

    void Start()
    {
        if (timeController == null)
            Debug.LogError("❌ TimeController not assigned in ZombieSpawner.");

        if (player == null)
            Debug.LogError("❌ Player reference missing in ZombieSpawner.");

        if (zombieTypes == null || zombieTypes.Count == 0)
            Debug.LogError("❌ Zombie types list is empty in ZombieSpawner.");

        wait = new WaitForSeconds(spawnInterval);
    }

    void Update()
    {
        if (timeController == null || player == null || zombieTypes == null || zombieTypes.Count == 0)
            return;

        TimeSpan currentTime = timeController.GetCurrentTime().TimeOfDay;
        TimeSpan nightStart = TimeSpan.FromHours(19);
        TimeSpan nightEnd = TimeSpan.FromHours(timeController.sunriseHour);

        bool currentlyNight = currentTime >= nightStart || currentTime < nightEnd;

        if (currentlyNight != isNight)
        {
            isNight = currentlyNight;

            if (isNight)
            {
                spawnCoroutine = StartCoroutine(SpawnZombies());
            }
            else
            {
                if (spawnCoroutine != null)
                    StopCoroutine(spawnCoroutine);

                ClearZombies();
            }
        }
    }

    private IEnumerator SpawnZombies()
    {
        while (isNight)
        {
            if (spawnedZombies.Count < maxZombiesAtOnce)
            {
                GameObject prefab = GetRandomZombiePrefab();
                Vector3 spawnPos = GetRandomSpawnPosition();

                if (prefab != null)
                {
                    GameObject zombie = Instantiate(prefab, spawnPos, Quaternion.identity);
                    spawnedZombies.Add(zombie);
                }
            }

            yield return wait;
        }
    }

    private GameObject GetRandomZombiePrefab()
    {
        int roll = UnityEngine.Random.Range(0, 100);
        int cumulative = 0;

        foreach (var type in zombieTypes)
        {
            cumulative += type.spawnChance;
            if (roll < cumulative && type.prefab != null)
                return type.prefab;
        }

        return zombieTypes[0].prefab;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDir = UnityEngine.Random.insideUnitCircle.normalized;
        float distance = UnityEngine.Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 position = player.position + new Vector3(randomDir.x, 0, randomDir.y) * distance;

        if (Physics.Raycast(position + Vector3.up * 50f, Vector3.down, out RaycastHit hit, 100f))
        {
            position.y = hit.point.y;
        }

        return position;
    }

    private void ClearZombies()
    {
        foreach (var zombie in spawnedZombies)
        {
            if (zombie != null)
                Destroy(zombie);
        }
        spawnedZombies.Clear();
    }
}
