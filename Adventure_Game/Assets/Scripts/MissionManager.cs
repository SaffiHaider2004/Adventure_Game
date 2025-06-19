using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    [Header("UI References")]
    public TMP_Text missionText;
    public TMP_Text appleCountText;

    [Header("Apple Collection")]
    private int appleCount = 0;
    public int targetAppleCount = 5;

    [Header("Zombie Kill")]
    private int zombiesKilled = 0;
    public int targetZombieCount = 3;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Debug.Log("✅ MissionManager initialized in scene: " + gameObject.name);

        if (GameStateManager.instance != null)
        {
            switch (GameStateManager.instance.currentPhase)
            {
                case GameStateManager.MissionPhase.CollectApples:
                    UpdateMissionText("Collect 5 apples");
                    UpdateUI();
                    break;

                case GameStateManager.MissionPhase.EscapeCave:
                    UpdateMissionText("Escape the cave");
                    if (appleCountText != null) appleCountText.text = "";
                    break;

                case GameStateManager.MissionPhase.KillZombies:
                    UpdateMissionText("Kill 3 zombies");
                    UpdateUI();
                    break;
            }
        }
    }

    // ---------------------- APPLE MISSION ----------------------
    public void CollectApple()
    {
        if (GameStateManager.instance.currentPhase != GameStateManager.MissionPhase.CollectApples)
            return;

        appleCount++;
        UpdateUI();

        if (appleCount >= targetAppleCount)
        {
            UpdateMissionText("Find the temple door");
            MissionComplete();
        }
    }

    public bool ApplesCollected()
    {
        return appleCount >= targetAppleCount;
    }

    // ---------------------- ZOMBIE MISSION ----------------------
    public void OnZombieKilled()
    {
        if (GameStateManager.instance.currentPhase != GameStateManager.MissionPhase.KillZombies)
            return;

        zombiesKilled++;
        UpdateUI();

        if (zombiesKilled >= targetZombieCount)
        {
            Debug.Log("✅ Zombie mission complete!");
            UpdateMissionText("Mission Complete!");

            // Restart loop after short delay
            Invoke("RestartMissionLoop", 2f);
        }
    }

    // ---------------------- UTIL ----------------------
    void UpdateUI()
    {
        if (appleCountText == null)
        {
            Debug.LogWarning("⚠️ AppleCountText is not assigned.");
            return;
        }

        if (GameStateManager.instance.currentPhase == GameStateManager.MissionPhase.CollectApples)
        {
            appleCountText.text = $"Apples: {appleCount} / {targetAppleCount}";
        }
        else if (GameStateManager.instance.currentPhase == GameStateManager.MissionPhase.KillZombies)
        {
            appleCountText.text = $"Zombies: {zombiesKilled} / {targetZombieCount}";
        }
    }

    void MissionComplete()
    {
        Debug.Log("Mission Complete!");
        // You can add effects or sounds here if needed
    }

    void UpdateMissionText(string message)
    {
        if (missionText != null)
        {
            missionText.text = message;
        }
    }

    // ---------------------- LOOP SYSTEM ----------------------
    public void RestartMissionLoop()
    {
        Debug.Log("🔁 Restarting mission loop...");

        // Reset mission state
        appleCount = 0;
        zombiesKilled = 0;

        GameStateManager.instance.currentPhase = GameStateManager.MissionPhase.CollectApples;

        // Load the correct forest scene again
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

        SceneManager.LoadScene(returnScene);
    }
}
