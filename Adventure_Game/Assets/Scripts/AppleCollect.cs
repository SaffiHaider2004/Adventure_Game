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

            Destroy(gameObject);
        }
    }


}
