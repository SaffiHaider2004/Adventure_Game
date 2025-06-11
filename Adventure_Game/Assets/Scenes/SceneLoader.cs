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
        SceneManager.LoadScene("Scene1"); // Replace with your actual scene name
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Scene2"); // Replace with your actual scene name
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene("Scene3"); // Replace with your actual scene name
    }
}
