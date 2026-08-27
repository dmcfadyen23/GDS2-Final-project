using UnityEngine;

public class TutorialPickup : MonoBehaviour
{
    public enum PickupType
    {
        Brush,
        Paint
    }

    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;

    [Header("Tutorial UI")]
    [SerializeField] private TutorialUI tutorialUI;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        gameObject.SetActive(false);

        if (pickupType == PickupType.Brush)
        {
            tutorialUI.ShowMessage("Pick up the three paints");
        }
        else if (pickupType == PickupType.Paint)
        {
            tutorialUI.CollectPaint();
        }
    }
}
