using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 playerPosition;
    private string enemyID;

    private bool tutorialStarted = false;

    private int paintsCollected = 0;

    private string defeatedEnemyID = "";

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

    public void DefeatEnemy()
    {
        defeatedEnemyID = enemyID;
    }

    public bool IsEnemyDefeated(string id)
    {
        return defeatedEnemyID == id;
    }

    //Tutorial Stuff
    public bool HasTutorialStarted()
    {
        return tutorialStarted;
    }

    public void ReturnToOverworld()
    {
        SceneManager.LoadScene("TutorialFloor");
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
