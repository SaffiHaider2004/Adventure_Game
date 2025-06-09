using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Button useButton;

    private InventoryItem item;
    private PlayerStats player;

    public void Setup(InventoryItem newItem, PlayerStats stats)
    {
        item = newItem;
        player = stats;
        icon.sprite = item.icon;

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(() =>
        {
            InventorySystem.Instance.UseItem(item, player);
            Destroy(gameObject);
        });
    }
}
