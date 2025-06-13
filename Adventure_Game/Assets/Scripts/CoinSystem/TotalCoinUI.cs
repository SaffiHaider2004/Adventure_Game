using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalCoinUI : MonoBehaviour
{
    public Text totalCoinText;

    void Start()
    {
        UpdateUI();
    }

    void OnEnable()
    {
        UpdateUI(); // this gets called again when returning to the scene
    }

    void UpdateUI()
    {
        int total = CoinManager.GetTotalCoins();
        if (totalCoinText != null)
            totalCoinText.text = "Total Coins: " + total;
    }
}
