using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;

    void Start()
    {
        int characterIndex = PlayerPrefs.GetInt("Characters");
        if (characterIndex >= 0 && characterIndex < characterPrefabs.Length)
        {
            GameObject prefab = characterPrefabs[characterIndex];
            GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            // Ensure the instantiated clone is set to active
            clone.SetActive(true);
        }
        else
        {
            Debug.LogError("Character index out of range or invalid.");
        }
    }
}
