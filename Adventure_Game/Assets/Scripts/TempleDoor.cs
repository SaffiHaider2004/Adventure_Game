using UnityEngine;

public class TempleDoor : MonoBehaviour
{
    public bool playerReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReached = true;

            if (MissionManager.instance != null && MissionManager.instance.ApplesCollected())
            {
                Debug.Log("Temple reached and apples collected.");
                // You can show a message or call GameManager here instead
            }
            else
            {
                Debug.Log("Collect 5 apples first!");
            }
        }
    }
}
