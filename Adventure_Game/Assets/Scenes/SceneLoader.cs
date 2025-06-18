using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartingScene()
    {
        SceneManager.LoadScene("Starting_Scene"); // Replace with your actual scene name
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Name_Scene"); // Replace with your actual scene name
    }
    public void SettingScene()
    {
        SceneManager.LoadScene(1); // Replace with your actual scene name
    }
    public void SelectionScene()
    {
        SceneManager.LoadScene("CharacterSelection"); // Replace with your actual scene name
    }
    public void LoadScene1()
    {
        CoinManager.Instance?.SetCurrentCharacter("Gingerbread");
        SceneManager.LoadScene("Scene1"); // Replace with your actual scene name
    }

    public void LoadScene2()
    {
        CoinManager.Instance?.SetCurrentCharacter("Sture");
        SceneManager.LoadScene("Scene2"); // Replace with your actual scene name
    }

    public void LoadScene3()
    {
        CoinManager.Instance?.SetCurrentCharacter("Eevee");
        SceneManager.LoadScene("Scene3"); // Replace with your actual scene name
    }
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll(); // ⚠️ Deletes everything stored in PlayerPrefs
        PlayerPrefs.Save();

        Debug.Log("🔁 All PlayerPrefs data reset.");
    }
}
