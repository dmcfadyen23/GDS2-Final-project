using UnityEngine;

public class RoomEntry : MonoBehaviour
{
    [SerializeField] private RoomManager roomManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        roomManager.EnterRoom();
    }
}
