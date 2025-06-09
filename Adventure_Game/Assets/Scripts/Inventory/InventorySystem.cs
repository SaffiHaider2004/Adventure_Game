using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddItem(InventoryItem item)
    {
        inventoryItems.Add(item);
        onInventoryChangedCallback?.Invoke();
    }

    public void UseItem(InventoryItem item, PlayerStats player)
    {
        if (item.itemType == InventoryItem.ItemType.Health)
            player.RestoreHealth(item.restorePercent);
        else if (item.itemType == InventoryItem.ItemType.Stamina)
            player.RestoreStamina(item.restorePercent);

        inventoryItems.Remove(item);
        onInventoryChangedCallback?.Invoke();
    }
}
