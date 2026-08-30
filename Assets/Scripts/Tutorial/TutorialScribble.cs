using UnityEngine;

public class TutorialScribble : MonoBehaviour
{
    [Header("Tutorial UI")]
    [SerializeField] private TutorialUI tutorialUI;

    [Header("Tutorial Message")]
    [TextArea(2, 5)]
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialUI.ShowMessage(message);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        tutorialUI.HideMessage();
    }
}
