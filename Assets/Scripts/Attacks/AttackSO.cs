using System;
using Attacks;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public string attackName = null;
    public int basePower = 0;
    private float damageDealt;
    public string gestureName;
    public Color colour;
    protected UIManager uiManager;
    public AudioClip attackSFX;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    public enum TargetingType
    {
        SINGLE_TARGET, AOE, SELF
    }

    public TargetingType targetingType;

    public float GetDamageDealt()
    {
        return damageDealt;
    }
    
    public virtual void Attack(AttackContext context)
    {
        int randomNumCheck;
        bool canAttack = true;
        string effectiveness = "";
        uiManager = FindAnyObjectByType<UIManager>();
        Entity target = context.target;
        damageDealt = basePower*(context.sourceUnit.attackStat/100);
        float actualDamageDealt = 0;
        
        // self buff/debuff check
        foreach (var buff in context.sourceUnit.buffs)
        {
            if (buff.name == "MagicCharge")
            {
                damageDealt *= 1.5f;
            }
        }
        foreach (var debuff in context.sourceUnit.debuffs)
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

        if (canAttack)
        {
            PlayAttackSFX();
            // random number for accuracy check
            Random accuracyCheck = new Random();
            randomNumCheck = accuracyCheck.Next(101);
            // if random num is lower than accuracy then hit. accuracy is a stat from 0-100. as percentage.
            if (randomNumCheck <= context.sourceUnit.accuracy)
            {
                PlayAttackSFX();

                switch (targetingType)
                {
                    case TargetingType.SINGLE_TARGET:
                        actualDamageDealt = target.TakeDamage(this, context);
                        if (target is Enemy)
                        {
                            Enemy enemyTarget = (Enemy)target;
                            if (enemyTarget.weakness.Contains(attackName))
                            {
                                effectiveness = "Good";
                            }
                            else if (enemyTarget.resistance == attackName)
                            {
                                effectiveness = "Bad";
                            }
                        }

                        context.animator.Play("AttackAnim");
                        uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and did " + actualDamageDealt + " damage to " + context.target.unitName;
                        if (effectiveness == "Good")
                        {
                            uiManager.battleLogText.text += "The attack was super effective!";
                        }
                        else if (effectiveness == "Bad")
                        {
                            uiManager.battleLogText.text += "The attack was not very effective...";
                        }
                        break;
                    case TargetingType.AOE:
                        Enemy[] enemies = FindObjectsByType<Enemy>();
                        int activeEnemies = 0;
                        context.animator.Play("AttackAnim");
                        foreach (var enemy in enemies)
                        {
                            if (enemy.gameObject.activeSelf)
                            {
                                actualDamageDealt = enemy.TakeDamage(this, context);
                                if (enemy.weakness.Contains(attackName))
                                {
                                    effectiveness = "Good";
                                }
                                else if (enemy.resistance == attackName)
                                {
                                    effectiveness = "Bad";
                                }
                                activeEnemies++;
                            }
                        }

                        

                        if (activeEnemies > 1)
                        {
                            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and did " + actualDamageDealt + " damage to all enemies. ";
                        }
                        else
                        {
                            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + " and did " + actualDamageDealt + " damage to " + context.target.unitName + ". ";
                        }

                        if (effectiveness == "Good")
                        {
                            uiManager.battleLogText.text += "The attack was super effective!";
                        }
                        else if (effectiveness == "Bad")
                        {
                            uiManager.battleLogText.text += "The attack was not very effective...";
                        }
                        
                        break;
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
        foreach (var buff in context.sourceUnit.buffs)
        {
            if (buff.name == "Regen")
            {
                context.sourceUnit.health += 10;
            }
        }

        foreach (var debuff in context.sourceUnit.debuffs)
        {
            if (debuff.name == "Poison")
            {
                context.sourceUnit.health -= 20;
            }
        }

        // reset accuracy
        context.sourceUnit.accuracy = 100;

        // reduce buff and debuff durations
        for (int i = 0; i < context.sourceUnit.buffs.Count;i++)
        {
            // if buff duration set to -1, infinite buff
            if (context.sourceUnit.buffs[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.buffs[i] = (context.sourceUnit.buffs[i].name, context.sourceUnit.buffs[i].duration - 1);
            // clear buff if duration reaches 0
            if (context.sourceUnit.buffs[i].duration == 0)
            {
                context.sourceUnit.buffs.Remove(context.sourceUnit.buffs[i]);
            }
        }
        for (int i = 0; i < context.sourceUnit.debuffs.Count;i++)
        {
            // if debuff duration set to -1, infinite duration
            if (context.sourceUnit.debuffs[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.debuffs[i] = (context.sourceUnit.debuffs[i].name, context.sourceUnit.debuffs[i].duration - 1);
            // clear debuff if duration reaches 0
            if (context.sourceUnit.debuffs[i].duration == 0)
            {
                context.sourceUnit.debuffs.Remove(context.sourceUnit.debuffs[i]);
            }
        }
        
        // status changes
        for (int i = 0; i < context.sourceUnit.statuses.Count;i++)
        {
            // if status duration set to -1, infinite duration
            if (context.sourceUnit.statuses[i].duration == -1)
            {
                continue;
            }
            context.sourceUnit.statuses[i] = (context.sourceUnit.statuses[i].name, context.sourceUnit.statuses[i].duration - 1);
            // clear status if duration reaches 0
            if (context.sourceUnit.statuses[i].duration == 0)
            {
                context.sourceUnit.statuses.Remove(context.sourceUnit.statuses[i]);
            }
        }
    }

    private void PlayAttackSFX()
    {
        if (attackSFX != null)
        {
            AudioSource.PlayClipAtPoint(attackSFX, Vector3.zero, sfxVolume);
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
