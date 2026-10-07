using System.IO;
using UnityEngine;

public class CombatManager : MonoBehaviour //Add combat stuff here, all it has is grabbing which enemy player is fighting and return to overworld
{
    [SerializeField] private GameObject enemySprite;
    private void Start()
    {
        string enemyID = GameManager.Instance.GetEnemyID();
        string enemyName = GameManager.Instance.GetEnemyName();
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
