using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int sessionCoins = 0;
    private string currentCharacter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Optional starter coins logic (disabled by default)
            // Uncomment to enable test coins once
            /*
            if (!PlayerPrefs.HasKey("StarterCoinsGiven"))
            {
                PlayerPrefs.SetInt("Coins_Total", 2500);
                PlayerPrefs.SetInt("StarterCoinsGiven", 1);
                PlayerPrefs.Save();
                Debug.Log("🪙 Starter coins granted!");
            }
            */
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --------------------------------------------
    // 🧍 CHARACTER SETUP
    // --------------------------------------------
    public void SetCurrentCharacter(string character)
    {
        currentCharacter = character;
        PlayerPrefs.SetString("SelectedCharacter", character);
        PlayerPrefs.Save();
    }

    public string GetCurrentCharacter()
    {
        if (string.IsNullOrEmpty(currentCharacter))
        {
            currentCharacter = PlayerPrefs.GetString("SelectedCharacter", "Gingerbread"); // Default
        }
        return currentCharacter;
    }

    // --------------------------------------------
    // 🪙 COIN SYSTEM
    // --------------------------------------------

    public void AddCoin(int amount)
    {
        sessionCoins += amount;
        UIManager.Instance?.UpdateCoinUI(sessionCoins);
    }

    public void SaveCoins()
    {
        string character = GetCurrentCharacter();
        if (string.IsNullOrEmpty(character)) return;

        string charKey = $"Coins_{character}";
        int currentCharTotal = PlayerPrefs.GetInt(charKey, 0);
        PlayerPrefs.SetInt(charKey, currentCharTotal + sessionCoins);

        int total = PlayerPrefs.GetInt("Coins_Total", 0);
        PlayerPrefs.SetInt("Coins_Total", total + sessionCoins);

        PlayerPrefs.Save();
        sessionCoins = 0;
    }

    public static int GetTotalCoins() => PlayerPrefs.GetInt("Coins_Total", 0);
    public static int GetCharacterCoins(string character) => PlayerPrefs.GetInt($"Coins_{character}", 0);

    // --------------------------------------------
    // 🏠 HOUSE SYSTEM
    // --------------------------------------------

    public static bool IsHouseOwned(string houseKey)
    {
        return PlayerPrefs.GetInt(houseKey, 0) == 1;
    }

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
