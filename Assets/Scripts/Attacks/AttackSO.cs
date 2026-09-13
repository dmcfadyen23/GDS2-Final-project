using System;
using Attacks;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public string attackName = null;
    public int basePower = 0;
    public string gestureName;
    private UIManager uiManager;

    public enum TargetingType
    {
        SINGLE_TARGET, BLAST, AOE, SELF,
    }

    public TargetingType targetingType;

    public void Attack(AttackContext context)
    {
        Entity target = context.target;
        float damageDealt = basePower*(target.attackStat/100);
        if (target is Enemy)
        {
            Enemy enemyTarget = (Enemy)target;
            if (enemyTarget.weakness == this.attackName)
            {
                damageDealt *= 2;
            }

            if (enemyTarget.resistance == this.attackName)
            {
                damageDealt *= 0.5f;
            }
        }

        target.health -= damageDealt;
        context.animator.Play("EnemyAttackEffect");
        uiManager.battleLogText.text = context.sourceUnit + " used " + attackName + " and did " + damageDealt + " damage to " + context.target;
        if (target as Enemy)
        {
            uiManager.UpdateEnemyHealthBar(target.health);
        }
        else
        {
            uiManager.UpdatePlayerHealthBar(target.health);
        }
        
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
