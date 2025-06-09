using System.Linq;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;             // Assign your apple prefab here
    public Transform[] spawnPoints;            // Drop all your empty GameObjects here
    public int numberOfApples = 5;             // How many apples to spawn

    void Start()
    {
        SpawnApples();
    }

    void SpawnApples()
    {
        // Shuffle the spawn points to get random selection without repeating
        Transform[] shuffled = spawnPoints.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < Mathf.Min(numberOfApples, shuffled.Length); i++)
        {
            Instantiate(applePrefab, shuffled[i].position, Quaternion.identity);
        }
    }
}
