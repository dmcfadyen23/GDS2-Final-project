using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("Room Doors")]
    [SerializeField] private GameObject[] doors;

    [Header("Room Enemies")]
    [SerializeField] private Enemy[] enemies;

    private bool roomActivated = false;
    private bool roomCleared = false;

    public void EnterRoom()
    {
        if (roomActivated)
            return;

        roomActivated = true;

        CloseDoors();

        CheckEnemies();
    }

    public void EnemyDefeated()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        if (!roomActivated)
            return;

        int remainingEnemies = 0;

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                remainingEnemies++;
            }
        }

        Debug.Log("Enemies remaining: " + remainingEnemies);

        if (remainingEnemies == 0)
        {
            ClearRoom();
        }
    }

    private void ClearRoom()
    {
        if (roomCleared)
            return;

        roomCleared = true;

        OpenDoors();

        Debug.Log("Room cleared! Doors opened.");
    }

    private void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(true);
            }
        }
    }

    private void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }
}
