using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotParent;
    public PlayerStats playerStats;
    public Button inventoryToggleButton;
    public Button closeButton; // 👈 Add this in Inspector

    void Start()
    {
        InventorySystem.Instance.onInventoryChangedCallback += UpdateUI;

        inventoryPanel.SetActive(false);

        inventoryToggleButton.onClick.AddListener(ToggleInventory);
        closeButton.onClick.AddListener(CloseInventory); // 👈 Hook up Close Button
    }

    void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);

        if (!isActive)
            UpdateUI(); // Only update if opening
    }

    void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    void UpdateUI()
    {
        // Clear old slots
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        // Add current items
        foreach (var item in InventorySystem.Instance.inventoryItems)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.GetComponent<InventorySlotUI>().Setup(item, playerStats);
        }
    }
}
