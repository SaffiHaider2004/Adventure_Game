using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]

public class CharacterInfo
{
    public int cost;
    public GameObject Character;

    public bool isBought
    {
        get
        {
            return PlayerPrefs.HasKey(Character.name) || cost <= 0;
        }
    }


}

public class PlayerSelection : MonoBehaviour
{
    public CharacterInfo[] allCharacters;

    int currentIndex;

    public GameObject selectButton;



    // Start is called before the first frame update
    void Start()
    {
        foreach (CharacterInfo item in allCharacters)
        {
            item.Character.SetActive(false);
        }
        UpdateInfo(currentIndex);
    }

    public void UpdateInfo(int CIndex)
    {
        allCharacters[currentIndex].Character.SetActive(false);
        currentIndex = CIndex;
        allCharacters[currentIndex].Character.SetActive(true);
        selectButton.SetActive(allCharacters[currentIndex].isBought);

    }


    public void SelectButtonClick()
    {
        PlayerPrefs.SetInt("Characters", currentIndex);
        SceneManager.LoadScene(3);
    }

    public void Next()
    {
        currentIndex++;
        if (currentIndex > allCharacters.Length - 1)
        {
            currentIndex = 0;
        }
        foreach (CharacterInfo item in allCharacters)
        {
            item.Character.SetActive(false);

        }
        UpdateInfo(currentIndex);
    }
    public void Previous()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = allCharacters.Length - 1;

        }
        foreach (CharacterInfo item in allCharacters)
        {
            item.Character.SetActive(false);

        }
        UpdateInfo(currentIndex);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
