using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TempleDoor templeDoor;

    public GameObject worldObjects;
    public GameObject caveLevel;
    public Transform caveSpawnPoint;

    private PlayerInventory playerInventory;
    private GameObject player;

    public bool caveEntered = false;

    void Start()
    {
        StartCoroutine(InitializePlayer());
    }

    IEnumerator InitializePlayer()
    {
        // Wait until the player is spawned
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory not found on player!");
        }
    }

    void Update()
    {
        if (playerInventory == null || templeDoor == null) return;

        if (!caveEntered && playerInventory.applesCollected >= 4 && templeDoor.playerReached)
        {
            EnterCaveLevel();
        }
    }

    void EnterCaveLevel()
    {
        caveEntered = true;
        Debug.Log("Entering Cave Level!");

        // Move player to cave spawn point
        player.transform.position = caveSpawnPoint.position;
        player.transform.rotation = caveSpawnPoint.rotation;

        // Switch environments
        worldObjects.SetActive(false);
        caveLevel.SetActive(true);
    }
}
