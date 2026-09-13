using System;
using Attacks;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public string attackName = null;
    public int basePower = 0;
    public string gestureName;

    [Header("Attack Sound Effects")]
    public AudioClip drawSound;
    public AudioClip completeSound;
    public AudioClip hitSound;

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
        uiManager.battleLogText.text = context.sourceUnit + " used " + attackName + " and did " + damageDealt + " damage to " + context.target;

    }

    public void PlayDrawSound()
    {
        if (AttackAudioManager.Instance != null)
        {
            AttackAudioManager.Instance.PlaySound(drawSound);
        }
    }

    public void PlayCompleteSound()
    {
        if (AttackAudioManager.Instance != null)
        {
            AttackAudioManager.Instance.PlaySound(completeSound);
        }
    }

    public void PlayHitSound()
    {
        if (AttackAudioManager.Instance != null)
        {
            AttackAudioManager.Instance.PlaySound(hitSound);
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
