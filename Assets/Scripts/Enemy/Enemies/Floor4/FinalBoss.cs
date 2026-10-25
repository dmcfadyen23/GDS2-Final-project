using System.Collections.Generic;
using Attacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FinalBoss : Enemy
{
    //  spell that does regular amount of damage, spell that buffs self damage, spell that deals lots of damage, spell that heals self
    [SerializeField] public List<AttackSO> attacks;
    // cycle through spells in order
    private int currentAttack;
    
    public TextMeshProUGUI customDialogue;
    private SpriteRenderer enemyBg;

    public override void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        attacks[currentAttack].Attack(attackContext);
        currentAttack++;
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageTaken = attack.GetDamageDealt();
        
        health -= damageTaken;
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdateEnemyHealthBar(health, enemyNum);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            // check if all enemies dead
            bool allDead = true;
            CombatManager combatManager = FindAnyObjectByType<CombatManager>();
            GameObject[] enemies = combatManager.GetAllEnemies();
            foreach (var VARIABLE in enemies)
            {
                if (VARIABLE.activeSelf)
                {
                    allDead = false;
                }
            }

            if (allDead)
            {
                CombatManager.WinCombat();
            }
        }
        return damageTaken;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // GameObject.FindWithTag("CustomDialogue").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
