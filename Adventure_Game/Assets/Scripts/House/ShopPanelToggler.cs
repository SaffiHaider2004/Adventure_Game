using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelToggler : MonoBehaviour
{
    public GameObject shopPanel;

    public void ToggleShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }

    public void CloseShopPanel()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }
}
