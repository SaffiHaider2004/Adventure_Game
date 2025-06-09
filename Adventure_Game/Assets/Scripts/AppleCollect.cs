using UnityEngine;

public class AppleCollect : MonoBehaviour
{
    public int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add health or score logic here
            Debug.Log("Apple collected!");

            // Optional: access player script and heal


            // Destroy the apple
            Destroy(gameObject);
        }
    }
}
