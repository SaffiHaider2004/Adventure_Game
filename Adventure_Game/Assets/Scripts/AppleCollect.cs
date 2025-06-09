using UnityEngine;

public class AppleCollect : MonoBehaviour
{
    public int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectApple();
            }

<<<<<<< HEAD
=======
            // Optional: access player script and heal


            // Destroy the apple
>>>>>>> 3e961bf532c3e0f620d77ddbdcd24a14a4c66937
            Destroy(gameObject);
        }
    }


}
