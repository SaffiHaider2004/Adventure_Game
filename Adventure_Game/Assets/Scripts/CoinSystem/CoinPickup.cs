using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 1; // Set dynamically by ZombieHealth

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.Instance?.AddCoin(coinAmount);
            Debug.Log("Picked up coin: " + coinAmount);
            Destroy(gameObject);
        }
    }
}
