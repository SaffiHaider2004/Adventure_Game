using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int sessionCoins = 0;
    private string currentCharacter;

    // FOR ZERO COINS
    ////
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// 
    /// FOR TESTING 2500 COINS
    ///

    //void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject);

    //        // 🪙 GIVE STARTER COINS ONLY ONCE
    //        if (!PlayerPrefs.HasKey("StarterCoinsGiven"))
    //        {
    //            PlayerPrefs.SetInt("Coins_Total", 2500); // 🧪 Set your desired starting amount
    //            PlayerPrefs.SetInt("StarterCoinsGiven", 1); // 🔒 Prevent repeating
    //            PlayerPrefs.Save();
    //            Debug.Log("🪙 Starter coins granted!");
    //        }
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}


    // Set from SceneLoader.cs
    public void SetCurrentCharacter(string character)
    {
        currentCharacter = character;
    }

    public string GetCurrentCharacter()
    {
        return currentCharacter;
    }

    // 🪙 Called when coin is picked up
    public void AddCoin(int amount)
    {
        sessionCoins += amount;
        UIManager.Instance?.UpdateCoinUI(sessionCoins);
    }

    // 💾 Called on death or scene exit
    public void SaveCoins()
    {
        if (string.IsNullOrEmpty(currentCharacter)) return;

        string charKey = $"Coins_{currentCharacter}";
        int currentCharTotal = PlayerPrefs.GetInt(charKey, 0);
        PlayerPrefs.SetInt(charKey, currentCharTotal + sessionCoins);

        int total = PlayerPrefs.GetInt("Coins_Total", 0);
        PlayerPrefs.SetInt("Coins_Total", total + sessionCoins);

        PlayerPrefs.Save();
        sessionCoins = 0;
    }

    // 💰 Shop system coin access
    public static int GetTotalCoins() => PlayerPrefs.GetInt("Coins_Total", 0);
    public static int GetCharacterCoins(string character) => PlayerPrefs.GetInt($"Coins_{character}", 0);

    // ✅ Check if house is owned (used by HouseSpawner/Shop)
    public static bool IsHouseOwned(string houseKey)
    {
        return PlayerPrefs.GetInt(houseKey, 0) == 1;
    }

    // 💸 Deduct coins when buying house
    public static bool TrySpendCoins(int amount)
    {
        int totalCoins = GetTotalCoins();
        if (totalCoins >= amount)
        {
            PlayerPrefs.SetInt("Coins_Total", totalCoins - amount);
            PlayerPrefs.Save();
            return true;
        }
        return false;
    }
}
