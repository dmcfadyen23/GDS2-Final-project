using System;
using Attacks;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public string attackName = null;
    public int basePower = 0;
    public string gestureName;
    public Color colour;
    protected UIManager uiManager;

    public enum TargetingType
    {
        SINGLE_TARGET, BLAST, AOE, SELF,
    }

    public TargetingType targetingType;

    public virtual void Attack(AttackContext context)
    {
        uiManager = FindAnyObjectByType<UIManager>();
        Entity target = context.target;
        float damageDealt = basePower*(target.attackStat/100);
        //checks for status/debuffs/buffs on enemy
        foreach (var buff in target.buff)
        {
            if (buff.name == "Shielded")
            {
                damageDealt *= 0.5f;
            }
        }
        
        // self buff check
        foreach (var buff in context.sourceUnit.buff)
        {
            if (buff.name == "Regen")
            {
                context.sourceUnit.health += 10;
            }
        }
        
        // weakness 
        if (target is Enemy)
        {
            Enemy enemyTarget = (Enemy)target;
            if (enemyTarget.status.Contains("Doused"))
            {
                enemyTarget.weakness = "Lightning Bolt";
            }
            if (enemyTarget.weakness == attackName)
            {
                damageDealt *= 2;
            }

            if (enemyTarget.resistance == attackName)
            {
                damageDealt *= 0.5f;
            }
        }

        
        
        // random number for accuracy check
        Random accuracyCheck = new Random();
        int checkNum = accuracyCheck.Next(101);
        // if random num is lower than accuracy then hit. accuracy is a stat from 0-100. as percentage.
        if (checkNum <= context.sourceUnit.accuracy)
        {
            target.health -= damageDealt;
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and did " + damageDealt + " damage to " + context.target.unitName;
            if (target as Enemy)
            {
                uiManager.UpdateEnemyHealthBar(target.health);
            }
            else
            {
                uiManager.UpdatePlayerHealthBar(target.health);
            }
        }
        else
        {
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and missed " + context.target.unitName;
        }

        context.sourceUnit.accuracy = 100;
        
        // reduce buff and debuff durations
        for (int i = 0; i < context.sourceUnit.buff.Count;i++)
        {
            // if buff duration set to -1, infinite buff
            if (context.sourceUnit.buff[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.buff[i] = (context.sourceUnit.buff[i].name, context.sourceUnit.buff[i].duration - 1);
            // clear buff if duration reaches 0
            if (context.sourceUnit.buff[i].duration == 0)
            {
                context.sourceUnit.buff.Remove(context.sourceUnit.buff[i]);
            }
        }
        for (int i = 0; i < context.sourceUnit.debuff.Count;i++)
        {
            // if debuff duration set to -1, infinite duration
            if (context.sourceUnit.debuff[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.debuff[i] = (context.sourceUnit.debuff[i].name, context.sourceUnit.debuff[i].duration - 1);
            // clear debuff if duration reaches 0
            if (context.sourceUnit.debuff[i].duration == 0)
            {
                context.sourceUnit.debuff.Remove(context.sourceUnit.debuff[i]);
            }
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
