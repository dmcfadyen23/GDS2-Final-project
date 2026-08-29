using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 playerPosition;
    private string enemyID;

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

        tutorialStarted = true;

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

    public void ReturnToOverworld()
    {
        SceneManager.LoadScene("TutorialFloor");
    }

    public void DefeatEnemy()
    {
        if (!string.IsNullOrEmpty(enemyID))
        {
            defeatedEnemies.Add(enemyID);
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
