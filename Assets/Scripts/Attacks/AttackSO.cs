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
        if (target is Enemy)
        {
            Enemy enemyTarget = (Enemy)target;
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
        Random AccuracyCheck = new Random();
        int checkNum = AccuracyCheck.Next(101);
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
