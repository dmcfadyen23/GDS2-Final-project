using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Knight : Enemy
{
    [SerializeField] private List<AttackSO> attacks;
    
    
    public override void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        basicAttack.Attack(attackContext);
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageTaken = 0;
        if (!weakness.Contains(attack.name))
        {
            damageTaken = 10;
        }
        else
        {
            damageTaken = 100;
        }
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
