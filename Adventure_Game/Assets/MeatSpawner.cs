using UnityEngine;

public class MeatSpawner : MonoBehaviour
{
    public GameObject meatPrefab;
    public float spawnInterval = 20f;
    public float spawnRadius = 10f;
    public LayerMask terrainLayer; // assign your terrain layer in the Inspector

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(SpawnMeat), 5f, spawnInterval);
    }

    void SpawnMeat()
    {
        if (player == null || meatPrefab == null) return;

        Vector3 offset = Random.insideUnitSphere * spawnRadius;
        offset.y = 7f; // raise Y to cast downward from above

        Vector3 spawnPos = player.transform.position + offset;

        // Cast ray down to find terrain height
        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 20f, terrainLayer))
        {
            spawnPos = hit.point;
            Instantiate(meatPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Meat spawn failed — no terrain found under spawn position.");
        }
    }
}
