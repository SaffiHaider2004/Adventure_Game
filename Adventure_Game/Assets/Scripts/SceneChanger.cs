using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void EnterGame()
    {
        SceneManager.LoadScene(0);

    }

    public void Settings()
    {
        SceneManager.LoadScene(1);

    }

    public void EnterName()
    {
        SceneManager.LoadScene(2);
    }
    public void GamePlay()
    {
        SceneManager.LoadScene(3);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(4);
    }
    public void CharacterSelection()
    {
        SceneManager.LoadScene(5);
    }

}