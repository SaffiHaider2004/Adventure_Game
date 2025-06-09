using UnityEngine;

public class TempleDoor : MonoBehaviour
{
    public int requiredApples = 4;
    public GameObject missionSuccessUI; // optional UI message

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && inventory.applesCollected >= requiredApples)
            {
                Debug.Log("Mission Successful!");

                if (missionSuccessUI != null)
                    missionSuccessUI.SetActive(true);

                // Optional: add sound, animation, or next level load
            }
            else
            {
                Debug.Log("Not enough apples.");
            }
        }
    }
}
