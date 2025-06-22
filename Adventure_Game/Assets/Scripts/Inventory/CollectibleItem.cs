using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public InventoryItem itemData;
    [HideInInspector] public Transform spawnPoint;
    [HideInInspector] public AppleSpawner spawner;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MissionManager.instance?.CollectApple();
            InventorySystem.Instance?.AddItem(itemData);

            if (spawner != null && spawnPoint != null)
            {
                spawner.SpawnAppleAt(spawnPoint, 30f); // respawn after 30 seconds
            }

            Destroy(gameObject);
        }
    }
}
