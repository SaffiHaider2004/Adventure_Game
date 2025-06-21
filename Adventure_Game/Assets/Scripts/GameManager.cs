using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TempleDoor templeDoor;

    public GameObject worldObjects;
    public GameObject caveLevel;
    public Transform caveSpawnPoint;
    public GameObject player;

    private bool caveEntered = false;

    void Update()
    {
        if (caveEntered) return;

        if (playerInventory != null && templeDoor != null &&
            playerInventory.applesCollected >= 4 && templeDoor.playerReached)
        {
            EnterCaveLevel();
        }
    }

    private void EnterCaveLevel()
    {
        caveEntered = true;
        Debug.Log("Entering Cave Level!");

        if (player != null && caveSpawnPoint != null)
        {
            player.transform.SetPositionAndRotation(caveSpawnPoint.position, caveSpawnPoint.rotation);
        }

        if (worldObjects != null) worldObjects.SetActive(false);
        if (caveLevel != null) caveLevel.SetActive(true);
    }
}
