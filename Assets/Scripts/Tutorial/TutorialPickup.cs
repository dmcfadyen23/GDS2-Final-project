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
        if (pickupType == PickupType.Brush && GameManager.Instance != null &&
            GameManager.Instance.HasCollectedBrush())
        {
            collected = true;
            gameObject.SetActive(false);
        }
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
        if (Time.timeScale == 0f)
            return;

        if (!other.CompareTag("Player"))
            return;

        collected = true;

        // Make the pickup disappear
        gameObject.SetActive(false);

        if (pickupType == PickupType.Brush)
        {
            GameManager.GetOrCreate().CollectBrush();
            tutorialUI.ShowMessage("Pick up the three paints");
        }
        else if (pickupType == PickupType.Paint)
        {
            GameManager.GetOrCreate().CollectPaint(paintID);

            tutorialUI.CollectPaint();
        }
    }
}
