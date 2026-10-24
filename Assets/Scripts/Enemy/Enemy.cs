using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Enemy : Entity //Add enemy stuff here
{
    // number of this enemy you will encounter when running into them
    public int numEnemies;
    
    // which enemy in-combat if multiple enemies (set in combat manager)
    private int enemyNum;
    [SerializeField] public string enemyID;
    [SerializeField] public AttackSO basicAttack;
    public List<string> weakness;
    public string resistance;
    public string immune;
    private AttackContext attackContext = new AttackContext();
    

    public string GetEnemyID()
    {
        return enemyID;
    }

    public int GetEnemyNum()
    {
        return enemyNum;
    }

    public void SetEnemyNum(int number)
    {
        enemyNum = number;
    }

    public AttackSO GetAttack()
    {
        return basicAttack;
    }

    public override void TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageTaken = attack.GetDamageDealt();
        if (weakness.Contains(attack.attackName))
        {
            damageTaken *= 2;
        }
        else if (attack.attackName == resistance)
        {
            damageTaken *= 0.5f;
        }
        else if (attack.attackName == immune)
        {
            damageTaken = 0;
        }
        
        foreach (var buff in buffs)
        {
            if (buff.name == "Shielded")
            {
                damageTaken *= 0.5f;
            }
        }

        foreach (var status in context.target.statuses)
        {
            if (status.name == "Protected")
            {
                damageTaken = 0;
            }
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

        
    }

    public void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        basicAttack.Attack(attackContext);
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsEnemyDefeated(enemyID))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
