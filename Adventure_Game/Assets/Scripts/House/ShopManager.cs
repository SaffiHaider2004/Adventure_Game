using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class HouseItem
    {
        public string key;           // e.g. "House_Ginger"
        public int price;            // e.g. 50
        public Button buyButton;
        public Text statusText;
    }

    public HouseItem[] houses;
    public Text totalCoinText;

    void OnEnable()
    {
        UpdateShopUI();
    }

    public void TryBuy(string houseKey)
    {
        HouseItem item = System.Array.Find(houses, h => h.key == houseKey);
        if (item == null) return;

        int coins = CoinManager.GetTotalCoins();
        if (CoinManager.IsHouseOwned(houseKey)) return;

        if (coins >= item.price)
        {
            PlayerPrefs.SetInt("Coins_Total", coins - item.price);
            PlayerPrefs.SetInt(houseKey, 1);
            PlayerPrefs.Save();
            UpdateShopUI();
        }
    }

    void UpdateShopUI()
    {
        int coins = CoinManager.GetTotalCoins();
        if (totalCoinText != null)
            totalCoinText.text = "Total Coins: " + coins;

        foreach (var item in houses)
        {
            bool owned = CoinManager.IsHouseOwned(item.key);
            item.statusText.text = owned ? "Owned" : $"Buy ({item.price})";
            item.buyButton.gameObject.SetActive(!owned);
        }
    }
}
