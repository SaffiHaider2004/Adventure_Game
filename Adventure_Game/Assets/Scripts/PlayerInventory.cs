using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int applesCollected = 0;

    public void CollectApple()
    {
        applesCollected++;
        Debug.Log("Apples: " + applesCollected);
    }
}
