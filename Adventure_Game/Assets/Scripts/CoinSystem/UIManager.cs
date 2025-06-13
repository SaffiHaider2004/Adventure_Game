using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text coinText; // or TMP_Text

    void Awake() => Instance = this;

    public void UpdateCoinUI(int amount)
    {
        if (coinText != null)
            coinText.text = "Coins: " + amount;
    }
}
