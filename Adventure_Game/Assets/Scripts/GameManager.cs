using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TempleDoor templeDoor;

    public GameObject worldObjects;
    public GameObject caveLevel;
    public Transform caveSpawnPoint;
    public GameObject player;

    public bool caveEntered = false;

    void Update()
    {
        if (!caveEntered && playerInventory.applesCollected >= 4 && templeDoor.playerReached)
        {
            EnterCaveLevel();
        }
    }

    void EnterCaveLevel()
    {
        caveEntered = true;
        Debug.Log("Entering Cave Level!");

        player.transform.position = caveSpawnPoint.position;
        player.transform.rotation = caveSpawnPoint.rotation;
        worldObjects.SetActive(false);
        caveLevel.SetActive(true);

    }
}
