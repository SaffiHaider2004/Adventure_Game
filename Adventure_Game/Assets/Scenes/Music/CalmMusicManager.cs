using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalmMusicManager : MonoBehaviour
{
    public static CalmMusicManager Instance;

    public AudioClip calmMusicClip;
    private AudioSource audioSource;

    // Add your gameplay scene names here
    private string[] gameplayScenes = { "Scene1", "Scene2", "Scene3" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = calmMusicClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.Play();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string gameplayScene in gameplayScenes)
        {
            if (scene.name == gameplayScene)
            {
                // Stop calm music in gameplay scenes
                audioSource.Stop();
                SceneManager.sceneLoaded -= OnSceneLoaded;
                Destroy(gameObject);
                return;
            }
        }
    }
}
