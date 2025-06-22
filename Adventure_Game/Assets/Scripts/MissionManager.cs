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
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Debug.Log("✅ MissionManager initialized in scene: " + SceneManager.GetActiveScene().name);

        if (GameStateManager.instance == null)
        {
            Debug.LogWarning("❌ GameStateManager not found.");
            return;
        }

        switch (GameStateManager.instance.currentPhase)
        {
            case GameStateManager.MissionPhase.CollectApples:
                UpdateMissionText("Collect 5 apples");
                UpdateUI();
                break;

            case GameStateManager.MissionPhase.EscapeCave:
                UpdateMissionText("Escape the cave");
                ClearUI();
                break;

            case GameStateManager.MissionPhase.KillZombies:
                UpdateMissionText("Kill 3 zombies");
                UpdateUI();
                break;
        }
    }

    // ---------------------- APPLE MISSION ----------------------
    public void CollectApple()
    {
        if (GameStateManager.instance.currentPhase != GameStateManager.MissionPhase.CollectApples) return;

        appleCount++;
        UpdateUI();

        if (appleCount >= targetAppleCount)
        {
            UpdateMissionText("Find the temple door");
            MissionComplete();
        }
    }

    public bool ApplesCollected() => appleCount >= targetAppleCount;

    // ---------------------- ZOMBIE MISSION ----------------------
    public void OnZombieKilled()
    {
        if (GameStateManager.instance.currentPhase != GameStateManager.MissionPhase.KillZombies) return;

        zombiesKilled++;
        UpdateUI();

        if (zombiesKilled >= targetZombieCount)
        {
            Debug.Log("✅ Zombie mission complete!");
            UpdateMissionText("Mission Complete!");
            Invoke(nameof(RestartMissionLoop), 2f);
        }
    }

    // ---------------------- UI ----------------------
    void UpdateUI()
    {
        if (appleCountText == null)
        {
            Debug.LogWarning("⚠️ AppleCountText is not assigned.");
            return;
        }

        switch (GameStateManager.instance.currentPhase)
        {
            case GameStateManager.MissionPhase.CollectApples:
                appleCountText.text = $"Apples: {appleCount} / {targetAppleCount}";
                break;

            case GameStateManager.MissionPhase.KillZombies:
                appleCountText.text = $"Zombies: {zombiesKilled} / {targetZombieCount}";
                break;
        }
    }

    void ClearUI()
    {
        if (appleCountText != null)
            appleCountText.text = "";
    }

    void UpdateMissionText(string message)
    {
        if (missionText != null)
            missionText.text = message;
    }

    void MissionComplete()
    {
        Debug.Log("🏁 Mission Complete!");
        // Add optional effects/audio
    }

    // ---------------------- LOOP SYSTEM ----------------------
    public void RestartMissionLoop()
    {
        Debug.Log("🔁 Restarting mission loop...");

        appleCount = 0;
        zombiesKilled = 0;
        GameStateManager.instance.currentPhase = GameStateManager.MissionPhase.CollectApples;

        // Reload the current scene — zombie phase already happens in forest
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
