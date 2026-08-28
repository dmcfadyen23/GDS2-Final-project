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

    [Header("Paint ID")]
    [SerializeField] private int paintID;

    [Header("Tutorial UI")]
    [SerializeField] private TutorialUI tutorialUI;

    private bool collected = false;

    private void Start()
    {
        // Paints check if they were already collected
        if (pickupType == PickupType.Paint)
        {
            if (GameManager.Instance != null &&
                GameManager.Instance.HasCollectedPaint(paintID))
            {
                collected = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        // Make the pickup disappear
        gameObject.SetActive(false);

        if (pickupType == PickupType.Brush)
        {
            tutorialUI.ShowMessage("Pick up the three paints");
        }
        else if (pickupType == PickupType.Paint)
        {
            GameManager.Instance.CollectPaint();

            tutorialUI.CollectPaint();
        }
    }
}
