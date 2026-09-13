using UnityEngine;
using System;
using Attacks;

[Serializable]
public class Attack
{
    public string attackName;
    public int damage;

    public Attack(string attackName, int damage)
    {
        this.attackName = attackName;
        this.damage = damage;
    }

    // public void UseAttack(AttackContext context)
    // {
    //     Entity target = context.target;
    //     float damageDealt = damage*(target.attackStat/100);
    //     if (target is Enemy)
    //     {
    //         Enemy enemyTarget = (Enemy)target;
    //         if (enemyTarget.weakness == this.attackName)
    //         {
    //             damageDealt *= 2;
    //         }
    //
    //         if (enemyTarget.resistance == this.attackName)
    //         {
    //             damageDealt *= 0.5f;
    //         }
    //     }
    //
    //     target.health -= damageDealt;
    //     UIManager uiManager = 
    //     uiManager.battleLogText.text = context.sourceUnit + " used " + attackName + " and did " + damageDealt + " damage to " + context.target;
    // }
}
