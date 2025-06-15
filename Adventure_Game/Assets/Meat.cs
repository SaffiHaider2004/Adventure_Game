using UnityEngine;

public class Meat : MonoBehaviour
{
    [Tooltip("Amount of health to restore as a percentage of max health (e.g., 0.1 = 10%)")]
    public float healPercent = 0.1f;
    public float despawnTime = 30f;

    private void Start()
    {
        // Automatically destroy the meat after 30 seconds if not collected
        Destroy(gameObject, despawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.RestoreHealth(healPercent);
                Debug.Log("Player healed by meat pickup.");
            }

            Destroy(gameObject); // Remove meat on pickup
        }
    }
}
