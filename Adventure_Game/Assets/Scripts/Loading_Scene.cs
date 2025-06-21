using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Scene : MonoBehaviour
{
    public GameObject Loading_Image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel(int n)
    {
        SceneManager.LoadScene(n);
    }
    public void LoadScene(int n)
    {
        StartCoroutine(LoadAsc(n));
        Loading_Image.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    IEnumerator LoadAsc(int n)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(n);
        while (! op.isDone) 
        {
            if (!Loading_Image.activeSelf)
                Loading_Image.SetActive(true);
            yield return null;
        }
    }

}
