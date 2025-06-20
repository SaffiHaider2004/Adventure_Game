using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public InventoryItem itemData;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventorySystem.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
