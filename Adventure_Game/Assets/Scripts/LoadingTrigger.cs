using UnityEngine;

public class LoadingTrigger : MonoBehaviour
{
    public GameObject loadingScreen;

    private void Start()
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Loading screen GameObject is not assigned!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // ✅ Only show loading screen if apples are collected
        if (MissionManager.instance != null && MissionManager.instance.ApplesCollected())
        {
            if (loadingScreen != null)
            {
                loadingScreen.SetActive(true);
                // Optional: Start loading the next scene here
                // StartCoroutine(LoadNextSceneAfterDelay(2f));
            }
        }
        else
        {
            Debug.Log("🛑 Player reached temple but hasn't collected all apples yet.");
        }
    }
}
