using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FloorExit : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private int sceneIndex;
#endif

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        LoadNextFloor();
    }

    private void LoadNextFloor()
    {
        SceneManager.LoadScene(sceneIndex);

        Debug.LogError("Next floor is not configured for this build.");

    }
}
