using UnityEngine;

public class TempleDoor : MonoBehaviour
{
    public bool playerReached = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerReached = true;
            Debug.Log("Player reached temple door.");
        }
    }
}
