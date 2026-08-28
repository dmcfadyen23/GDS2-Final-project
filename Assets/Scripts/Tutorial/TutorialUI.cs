using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;

    private void Start()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.HasTutorialStarted())
        {
            tutorialText.text = "";
        }
    }

    public void ShowMessage(string message)
    {
        tutorialText.text = message;
    }

    public void HideMessage()
    {
        tutorialText.text = "";
    }

    public void CollectPaint()
    {
        int paintsCollected = GameManager.Instance.GetPaintsCollected();

        if (paintsCollected >= 3)
        {
            ShowMessage("Read the scrolls on the walls");
        }
        else
        {
            ShowMessage("Pick up the three paints");
        }
    }
}
