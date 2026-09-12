using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FloorExit : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextFloor;
#endif

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        LoadNextFloor();
    }

    private void LoadNextFloor()
    {
#if UNITY_EDITOR
        if (nextFloor == null)
        {
            Debug.LogError("No next floor has been assigned!");
            return;
        }

        string scenePath = AssetDatabase.GetAssetPath(nextFloor);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        Debug.Log("Loading next floor: " + sceneName);

        SceneManager.LoadScene(sceneName);
#else
        Debug.LogError("Next floor is not configured for this build.");
#endif
    }
}
