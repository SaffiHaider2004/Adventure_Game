using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public enum ItemType { Health, Stamina }
    public ItemType itemType;
    [Range(0f, 1f)] public float restorePercent = 0.2f;
}
