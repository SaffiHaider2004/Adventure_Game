using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalCoinUI : MonoBehaviour
{
    public static TotalCoinUI Instance;
    public Text totalCoinText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshTotalCoins();
    }

    public void RefreshTotalCoins()
    {
        int total = CoinManager.GetTotalCoins();
        if (totalCoinText != null)
            totalCoinText.text = "Total Coins: " + total;

        Debug.Log("🪙 Total Coin UI Updated: " + total);
    }
}
