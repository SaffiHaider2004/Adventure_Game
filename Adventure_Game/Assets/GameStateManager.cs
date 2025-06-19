using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public enum MissionPhase
    {
        CollectApples,
        EscapeCave,
        KillZombies
    }

    public MissionPhase currentPhase = MissionPhase.CollectApples;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
