using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 playerPosition;

    private string enemyID;

    private string currentFloor;

    private bool tutorialStarted = false;

    private int paintsCollected = 0;

    private HashSet<string> defeatedEnemies = new HashSet<string>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void StartCombat(Vector3 position, string enemy)
    {
        playerPosition = position;

        enemyID = enemy;

        currentFloor = SceneManager.GetActiveScene().name;

        tutorialStarted = true;

        Debug.Log("Starting combat with: " + enemyID);
        Debug.Log("Returning to scene: " + currentFloor);

        SceneManager.LoadScene("CombatView");
    }


    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }


    public string GetEnemyID()
    {
        return enemyID;
    }


    public string GetCurrentFloor()
    {
        return currentFloor;
    }

    public void ReturnToOverworld()
    {
        Debug.Log("Returning to: " + currentFloor);

        SceneManager.LoadScene(currentFloor);
    }

    public void DefeatEnemy()
    {
        if (!string.IsNullOrEmpty(enemyID))
        {
            defeatedEnemies.Add(enemyID);

            Debug.Log("Enemy defeated: " + enemyID);
        }
    }


    public bool IsEnemyDefeated(string id)
    {
        return defeatedEnemies.Contains(id);
    }

    public bool HasTutorialStarted()
    {
        return tutorialStarted;
    }

    public void CollectPaint()
    {
        paintsCollected++;

        Debug.Log("Paints collected: " + paintsCollected);
    }


    public int GetPaintsCollected()
    {
        return paintsCollected;
    }


    public bool HasCollectedPaint(int paintID)
    {
        return paintID <= paintsCollected;
    }
}
