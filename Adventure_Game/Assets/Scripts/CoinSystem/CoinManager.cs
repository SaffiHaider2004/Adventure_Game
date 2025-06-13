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
        }
        else Destroy(gameObject);
    }

    public void SetCurrentCharacter(string character)
    {
        currentCharacter = character;
    }

    public void AddCoin(int amount)
    {
        sessionCoins += amount;
        UIManager.Instance?.UpdateCoinUI(sessionCoins);
    }

    public void SaveCoins()
    {
        string charKey = $"Coins_{currentCharacter}";
        int currentCharTotal = PlayerPrefs.GetInt(charKey, 0);
        PlayerPrefs.SetInt(charKey, currentCharTotal + sessionCoins);

        int total = PlayerPrefs.GetInt("Coins_Total", 0);
        PlayerPrefs.SetInt("Coins_Total", total + sessionCoins);

        PlayerPrefs.Save();
        sessionCoins = 0;
    }

    public static int GetTotalCoins() => PlayerPrefs.GetInt("Coins_Total", 0);
    public static int GetCharacterCoins(string character) => PlayerPrefs.GetInt($"Coins_{character}", 0);
}
