using System;
using Attacks;
using UnityEngine;

public class AttackSO : ScriptableObject
{
    public string attackName = null;
    public int basePower = 0;

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
