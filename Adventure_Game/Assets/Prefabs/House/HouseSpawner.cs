using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    public GameObject gingerHousePrefab;
    public GameObject catHousePrefab;
    public GameObject dogHousePrefab;

    void Start()
    {
        string currentCharacter = CoinManager.Instance?.GetCurrentCharacter();

        if (currentCharacter == "Gingerbread" && CoinManager.IsHouseOwned("House_Ginger"))
            gingerHousePrefab.SetActive(true);
        else if (currentCharacter == "Sture" && CoinManager.IsHouseOwned("House_Cat"))
            catHousePrefab.SetActive(true);
        else if (currentCharacter == "Eevee" && CoinManager.IsHouseOwned("House_Dog"))
            dogHousePrefab.SetActive(true);
    }
}
