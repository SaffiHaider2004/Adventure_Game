using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableItem : MonoBehaviour
{
    public enum ItemType { Health, Stamina }
    public ItemType itemType;

    [Range(0f, 1f)] public float restorePercent = 0.2f; // 20%

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                if (itemType == ItemType.Health)
                    stats.RestoreHealth(restorePercent);
                else if (itemType == ItemType.Stamina)
                    stats.RestoreStamina(restorePercent);

                Destroy(gameObject); // remove food after pickup
            }
        }
    }
}
