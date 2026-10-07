using System.IO;
using UnityEngine;

public class CombatManager : MonoBehaviour //Add combat stuff here, all it has is grabbing which enemy player is fighting and return to overworld
{
    [SerializeField] private GameObject enemySprite;
    private void Start()
    {
        string enemyID = GameManager.Instance.GetEnemyID();
        string enemyName = GameManager.Instance.GetEnemyName();
        Enemy enemyEncountered = GameManager.Instance.GetEnemy();
        Enemy enemyScene = enemySprite.AddComponent<Enemy>();
        enemyScene.unitName = enemyEncountered.unitName;
        enemyScene.health = enemyEncountered.health;
        enemyScene.attackStat = enemyEncountered.attackStat;
        enemyScene.enemyID = enemyEncountered.enemyID;
        enemyScene.basicAttack = enemyEncountered.basicAttack;
        enemyScene.immune = enemyEncountered.immune;
        enemyScene.resistance = enemyEncountered.resistance;
        enemyScene.weakness = enemyEncountered.weakness;
        
        string spriteFilepath = "Sprites/"+enemyName;
        Debug.Log(spriteFilepath);
        SpriteRenderer spriteRenderer = enemySprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(spriteFilepath);

        Debug.Log("Starting combat against: " + enemyID);
    }

    public static void WinCombat()
    {
        GameManager.Instance.DefeatEnemy();

        GameManager.Instance.ReturnToOverworld();
    }

    public static void LoseCombat()
    {
        GameManager.Instance.ReturnToOverworld();
    }
}
