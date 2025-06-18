using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public TMP_Text missionText; // or TMP_Text if using TMP


    private int appleCount = 0;
    public int targetAppleCount = 5;

    // Reference to the UI Text
    public TMP_Text appleCountText; // for regular UI
    // public TMP_Text appleCountText; // use this line if using TextMeshPro

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        
        
            appleCount = 0;
            UpdateMissionText("Collect 5 apples");
        

    }

    public void CollectApple()
    {
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
        if (appleCountText != null)
        {
            appleCountText.text = "Apples: " + appleCount + " / " + targetAppleCount;
        }
    }
    public bool ApplesCollected()
    {
        return appleCount >= targetAppleCount;
    }

    void MissionComplete()
    {
        Debug.Log("Mission Complete!");
        // Add your next step logic here
    }
    void UpdateMissionText(string message)
    {
        if (missionText != null)
        {
            missionText.text = message;
        }
    }

}
