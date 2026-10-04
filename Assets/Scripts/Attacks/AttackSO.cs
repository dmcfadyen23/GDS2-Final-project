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
        int randomNumCheck;
        bool canAttack = true;
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

        foreach (var status in target.status)
        {
            if (status.name == "Protected")
            {
                damageDealt = 0;
            }
        }
        
        // self buff/debuff check
        foreach (var buff in context.sourceUnit.buff)
        {
            if (buff.name == "MagicCharge")
            {
                damageDealt *= 1.5f;
            }
        }
        foreach (var debuff in context.sourceUnit.debuff)
        {
            if (debuff.name == "Paralyze")
            {
                Random moveCheck = new Random();
                randomNumCheck = moveCheck.Next(101);
                if (randomNumCheck <= 30)
                {
                    canAttack = false;
                }
            }

            if (debuff.name == "Charmed")
            {
                damageDealt *= 0.5f;
            }
        }
        
        
        // weakness 
        if (target is Enemy)
        {
            Enemy enemyTarget = (Enemy)target;
            if (enemyTarget.debuff.Contains(("Doused", -1)))
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


        if (canAttack)
        {
            // random number for accuracy check
            Random accuracyCheck = new Random();
            randomNumCheck = accuracyCheck.Next(101);
            // if random num is lower than accuracy then hit. accuracy is a stat from 0-100. as percentage.
            if (randomNumCheck <= context.sourceUnit.accuracy)
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
                // CHANGE TO MISSED ATTACK ANIM WHEN IMPLEMENTED
                context.animator.Play("AttackAnim");
                uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and missed " + context.target.unitName;
            }
        }
        else
        {
            //CHANGE TO FAILED ATTACK ANIM WHEN IMPLEMENTED
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " was unable to move this turn!";
        }
        
        
        EndOfTurn(context);
    }

    public void EndOfTurn(AttackContext context)
    {
        foreach (var buff in context.sourceUnit.buff)
        {
            if (buff.name == "Regen")
            {
                context.sourceUnit.health += 10;
            }
        }

        // reset accuracy
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
        
        // status changes
        for (int i = 0; i < context.sourceUnit.status.Count;i++)
        {
            // if status duration set to -1, infinite duration
            if (context.sourceUnit.status[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.status[i] = (context.sourceUnit.status[i].name, context.sourceUnit.status[i].duration - 1);
            // clear status if duration reaches 0
            if (context.sourceUnit.status[i].duration == 0)
            {
                context.sourceUnit.status.Remove(context.sourceUnit.status[i]);
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
