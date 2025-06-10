using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] Characters;
    public int Number;

    public void ChangeCharacter(int Num)
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }

        Number += Num;
        if (Number > Characters.Length - 1)
            Number = 0;
        if (Number < 0)
            Number = Characters.Length - 1;

        Characters[Number].SetActive(true);
    }

    public void SelectCharacterButton()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", Number);
        PlayerPrefs.Save();
        SceneManager.LoadScene("fwOF_FreeDemo_OldForest"); // Use your actual gameplay scene name
    }
}
