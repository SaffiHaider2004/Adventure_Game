using UnityEngine;
using System.Collections;

public class MeatSpawner : MonoBehaviour
{
    public GameObject meatPrefab;
    public float spawnInterval = 20f;
    public float spawnRadius = 10f;
    public LayerMask terrainLayer;

    private GameObject player;
    private WaitForSeconds wait;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("❌ Player not found. Ensure the player has the 'Player' tag.");
            return;
        }

        if (meatPrefab == null)
        {
            Debug.LogError("❌ Meat prefab not assigned in MeatSpawner.");
            return;
        }

        wait = new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnMeatRoutine());
    }

    private IEnumerator SpawnMeatRoutine()
    {
        yield return new WaitForSeconds(5f); // initial delay

        while (true)
        {
            SpawnMeat();
            yield return wait;
        }
    }

    private void SpawnMeat()
    {
        Vector3 offset = Random.insideUnitSphere * spawnRadius;
        offset.y = 7f; // cast from above the terrain

        Vector3 spawnPos = player.transform.position + offset;

        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 20f, terrainLayer))
        {
            Instantiate(meatPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("⚠️ Meat spawn failed — no terrain under spawn position.");
        }
    }
}
