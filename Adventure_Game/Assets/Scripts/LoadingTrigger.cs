using UnityEngine;

public class LoadingTrigger : MonoBehaviour
{
    
    public GameObject loadingScreen;

    private void Start()
    {
        // Make sure the loading screen is disabled at the start
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
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            if (loadingScreen != null)
            {
                loadingScreen.SetActive(true);

                // OPTIONAL: Load the next scene after a delay (uncomment below to use)
                // StartCoroutine(LoadNextSceneAfterDelay(2f)); // e.g. 2 seconds
            }
        }
    }

    // OPTIONAL coroutine if you want to load a new scene after showing the loading screen
    /*
    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("YourSceneName"); // Replace with your actual scene name or build index
    }
    */
}
