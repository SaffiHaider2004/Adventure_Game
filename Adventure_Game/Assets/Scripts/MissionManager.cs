using UnityEngine;
using TMPro;

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
                    UpdateUI(); // show apple counter
                    break;

                case GameStateManager.MissionPhase.EscapeCave:
                    UpdateMissionText("Escape the cave");
                    appleCountText.text = ""; // clear UI
                    break;

                case GameStateManager.MissionPhase.KillZombies:
                    UpdateMissionText("Kill 3 zombies");
                    appleCountText.text = $"Zombies: {zombiesKilled} / {targetZombieCount}";
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

    void UpdateUI()
    {
        if (appleCountText == null)
        {
            Debug.LogWarning("⚠️ AppleCountText is not assigned in MissionManager.");
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
            Debug.Log("Zombie mission complete!");
            UpdateMissionText("Mission Complete!");
            // Add logic for next steps (victory screen, cutscene, etc.)
        }
    }

    // ---------------------- UTIL ----------------------
    void MissionComplete()
    {
        Debug.Log("Mission Complete!");
        // You can add door opening, triggers, or UI win here
    }

    void UpdateMissionText(string message)
    {
        if (missionText != null)
        {
            missionText.text = message;
        }
    }

}
