using UnityEngine;

public class CombatManager : MonoBehaviour //Add combat stuff here, all it has is grabbing which enemy player is fighting and return to overworld
{
    private void Start()
    {
        string enemyID = GameManager.Instance.GetEnemyID();

        Debug.Log("Starting combat against: " + enemyID);
    }

    public void WinCombat()
    {
        GameManager.Instance.DefeatEnemy();
        GameManager.Instance.ReturnToOverworld();
    }
}
