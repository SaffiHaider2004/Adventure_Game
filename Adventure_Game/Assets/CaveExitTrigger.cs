using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExitTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Set next mission phase
        if (GameStateManager.instance != null)
        {
            GameStateManager.instance.currentPhase = GameStateManager.MissionPhase.KillZombies;
        }

        // Detect which forest scene to return to based on current cave
        string currentScene = SceneManager.GetActiveScene().name;
        string returnScene = "";

        switch (currentScene)
        {
            case "Demo_Scene":
                returnScene = "Scene1";
                break;
            case "cat_cave":
                returnScene = "Scene2";
                break;
            case "Dog_Cave":
                returnScene = "Scene3";
                break;
            default:
                Debug.LogWarning("Unknown cave scene: " + currentScene);
                return;
        }

        Debug.Log("🔁 Returning to forest scene: " + returnScene);
        SceneManager.LoadScene(returnScene);
    }
}
