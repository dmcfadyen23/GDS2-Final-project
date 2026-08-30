using UnityEngine;

public class PlayerPositionRestore : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance == null)
            return;

        transform.position = GameManager.Instance.GetPlayerPosition();
    }
}
