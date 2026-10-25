
using System.IO;
using UnityEngine;

public class CombatManager : MonoBehaviour //Add combat stuff here, all it has is grabbing which enemy player is fighting and return to overworld
{
    [SerializeField] private GameObject[] enemies = new GameObject[3];
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private AudioSource bgmAudioSource;
    private int numEnemies;
    private static CombatManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();

        string enemyID = GameManager.Instance.GetEnemyID();
        string enemyName = GameManager.Instance.GetEnemyName();
        numEnemies = GameManager.Instance.GetNumEnemies();
        Enemy enemyEncountered = GameManager.Instance.GetEnemy();
        string spriteFilepath = "Sprites/"+enemyName;
        for (int i = 0; i < numEnemies; i++)
        {
            enemies[i].SetActive(true);

            uiManager.enemyHealthBars[i].gameObject.SetActive(true);

            Enemy enemyScene = enemies[i].AddComponent<Enemy>();
            enemyScene.SetEnemyNum(i);
            enemyScene.unitName = enemyEncountered.unitName;
            enemyScene.health = enemyEncountered.health;
            enemyScene.attackStat = enemyEncountered.attackStat;
            enemyScene.enemyID = enemyEncountered.enemyID;
            enemyScene.basicAttack = enemyEncountered.basicAttack;
            enemyScene.immune = enemyEncountered.immune;
            enemyScene.resistance = enemyEncountered.resistance;
            enemyScene.weakness = enemyEncountered.weakness;
            SpriteRenderer spriteRenderer = enemyScene.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>(spriteFilepath);
            uiManager.enemyHealthBars[i].maxValue = enemyScene.health;
        }

        Debug.Log("Starting combat against: " + enemyID);
    }

    public GameObject GetEnemy(int index)
    {
        return enemies[index];
    }

    public GameObject[] GetAllEnemies()
    {
        return enemies;
    }

    public GameObject[] GetAllActiveEnemies()
    {
        GameObject[] activeEnemies = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            if (enemies[i].activeSelf)
            {
                activeEnemies[i] = enemies[i];
            }
        }

        return activeEnemies;
    }

    public static void WinCombat()
    {
        GameManager.Instance.DefeatEnemy();

        if (instance != null)
        {
            instance.StartCoroutine(
                instance.PlayResultSoundAndReturn(instance.victorySound)
            );
        }
        else
        {
            GameManager.Instance.ReturnToOverworld();
        }
    }

    public static void LoseCombat()
    {
        if (instance != null)
        {
            instance.StartCoroutine(
                instance.PlayResultSoundAndReturn(instance.defeatSound)
            );
        }
        else
        {
            GameManager.Instance.ReturnToOverworld();
        }
    }

    private System.Collections.IEnumerator PlayResultSoundAndReturn(AudioClip clip)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);

            // Waits for sfx to finish playing
            yield return new WaitForSeconds(clip.length);
        }

        // Returns overworld after the sound 
        GameManager.Instance.ReturnToOverworld();
    }
}
