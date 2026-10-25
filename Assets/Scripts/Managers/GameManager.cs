using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 playerPosition;

    private Enemy enemy;
    private string enemyID;
    private string enemyName;
    private int numEnemies;

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

    public void StartCombat(Vector3 position, Enemy tempEnemy)
    {
        playerPosition = position;

        enemy = tempEnemy;

        currentFloor = SceneManager.GetActiveScene().name;

        tutorialStarted = true;

        Debug.Log("Starting combat with: " + GetEnemyID());
        Debug.Log("Returning to scene: " + GetCurrentFloor());

        switch (enemy.TypeOfEnemy)
        {
            case Enemy.EnemyType.ENEMY:
                SceneManager.LoadScene("CombatView");
                break;
            case Enemy.EnemyType.MINIBOSS:
                SceneManager.LoadScene("CombatViewMiniBosses");
                break;
            case Enemy.EnemyType.BOSS:
                SceneManager.LoadScene("FinalBossCombatView");
                break;
        }
        // SceneManager.LoadScene("CombatView");
    }


    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public Enemy GetEnemy()
    {
        return enemy;
    }
    public string GetEnemyID()
    {
        return enemy.enemyID;
    }

    public string GetEnemyName()
    {
        return enemy.unitName;
    }

    public int GetNumEnemies()
    {
        return enemy.numEnemies;
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
        if (!string.IsNullOrEmpty(GetEnemyID()))
        {
            defeatedEnemies.Add(GetEnemyID());
            

            Debug.Log("Enemy defeated: " + GetEnemyID());
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
