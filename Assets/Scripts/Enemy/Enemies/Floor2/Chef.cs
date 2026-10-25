using System.Collections.Generic;
using Attacks;
using UnityEngine;
using UnityEngine.UI;

public class Chef : Enemy
{
    [SerializeField] private List<AttackSO> attacks;
    private enum OvenStatus
    {
        Dull, Fire, LargeFire, Explosion
    }

    private OvenStatus ovenStatus = OvenStatus.Fire;
    [SerializeField] private Text customText;
    
    public override void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        basicAttack.Attack(attackContext);
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageTaken = 10;
        if (attack.attackName == "Fireball")
        {
            attackStat = 150;
            switch (ovenStatus)
            {
                case OvenStatus.LargeFire:
                    ovenStatus++;
                    
                    break;
            }
        }
        else
        {
            
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
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
