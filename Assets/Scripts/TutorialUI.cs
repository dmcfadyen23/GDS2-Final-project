using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;

    private int paintsCollected = 0;

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
        paintsCollected++;

        if (paintsCollected <= 3)
        {
            ShowMessage("Read the scrolls on the walls");
        }
        else
        {
            ShowMessage("Pick up the three paints");
        }
    }
}
