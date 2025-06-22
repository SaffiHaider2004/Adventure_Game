using System.Collections;
using System.Linq;
using UnityEngine;


public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;
    public Transform[] spawnPoints;
    public int numberOfApples = 5;

    void Start()
    {
        SpawnApples();
    }

    void SpawnApples()
    {
        Transform[] shuffled = spawnPoints.OrderBy(x => Random.value).ToArray();

        for (int i = 0; i < Mathf.Min(numberOfApples, shuffled.Length); i++)
        {
            SpawnAppleAt(shuffled[i]);
        }
    }

    public void SpawnAppleAt(Transform point, float delay = 0f)
    {
        if (delay > 0f)
        {
            StartCoroutine(RespawnAfterDelay(point, delay));
        }
        else
        {
            GameObject apple = Instantiate(applePrefab, point.position, Quaternion.identity);
            apple.GetComponent<CollectibleItem>().spawnPoint = point;
            apple.GetComponent<CollectibleItem>().spawner = this;
        }
    }

    private IEnumerator RespawnAfterDelay(Transform point, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnAppleAt(point);
    }
}
