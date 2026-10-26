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
    private HashSet<int> collectedPaintIDs = new HashSet<int>();
    private bool brushCollected;
    private bool restorePlayerPosition;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartCombat(Vector3 position, Enemy tempEnemy)
    {
        if (Time.timeScale == 0f) return;
        restorePlayerPosition = false;
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
        restorePlayerPosition = true;
        
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

    public static GameManager GetOrCreate()
    {
        if (Instance == null)
        {
            Instance = FindAnyObjectByType<GameManager>();
            if (Instance == null)
                Instance = new GameObject("GameManager").AddComponent<GameManager>();
        }
        return Instance;
    }

    public void ResetProgress()
    {
        playerPosition = Vector3.zero;
        currentFloor = null;
        enemy = null;
        enemyID = null;
        enemyName = null;
        numEnemies = 0;
        tutorialStarted = false;
        paintsCollected = 0;
        brushCollected = false;
        restorePlayerPosition = false;
        defeatedEnemies.Clear();
        collectedPaintIDs.Clear();
    }

    public GameSaveData CaptureProgress(string scene, Vector3 position)
    {
        return new GameSaveData
        {
            scene = scene, position = position,
            tutorialStarted = tutorialStarted, paintsCollected = paintsCollected,
            brushCollected = brushCollected,
            defeatedEnemies = new List<string>(defeatedEnemies),
            collectedPaintIDs = new List<int>(collectedPaintIDs)
        };
    }

    public void RestoreProgress(GameSaveData data)
    {
        ResetProgress();
        currentFloor = data.scene;
        playerPosition = data.position;
        tutorialStarted = data.tutorialStarted;
        paintsCollected = data.paintsCollected;
        brushCollected = data.brushCollected;
        defeatedEnemies = new HashSet<string>(data.defeatedEnemies);
        collectedPaintIDs = new HashSet<int>(data.collectedPaintIDs);
        restorePlayerPosition = true;
    }

    public bool TryGetRestorePosition(string scene, out Vector3 position)
    {
        position = playerPosition;
        return restorePlayerPosition && currentFloor == scene;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (restorePlayerPosition && scene.name == currentFloor)
            StartCoroutine(FinishPositionRestore());
    }

    private System.Collections.IEnumerator FinishPositionRestore()
    {
        yield return null;
        restorePlayerPosition = false;
    }

    private void OnDestroy()
    {
        if (Instance != this) return;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
    }

    public void CollectBrush() { brushCollected = true; }
    public bool HasCollectedBrush() { return brushCollected; }

    public void CollectPaint(int paintID)
    {
        if (collectedPaintIDs.Add(paintID)) CollectPaint();
    }
}