using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleDoor : MonoBehaviour
{
    public bool playerReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerReached = true;

        if (MissionManager.instance != null && MissionManager.instance.ApplesCollected())
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string caveSceneToLoad = "";

            switch (currentScene)
            {
                case "Scene1": // Gingerbread
                    caveSceneToLoad = "Demo_Scene";
                    break;
                case "Scene2": // Cat
                    caveSceneToLoad = "cat_cave";
                    break;
                case "Scene3": // Dog
                    caveSceneToLoad = "Dog_Cave";
                    break;
                default:
                    Debug.LogWarning("Unknown forest scene: " + currentScene);
                    return;
            }

            Debug.Log("✅ Loading cave scene: " + caveSceneToLoad);
            SceneManager.LoadScene(caveSceneToLoad);
        }
        else
        {
            Debug.Log("❌ You must collect 5 apples first!");
        }
    }
}
