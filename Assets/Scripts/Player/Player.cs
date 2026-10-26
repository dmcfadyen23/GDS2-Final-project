using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Player : Entity
{
    public List<AttackSO> possibleAttacks;

    private AttackContext attackContext = new AttackContext();

    private int playerLevel;

    public bool UseAttack(string gestureName, Color color)
    {
        if (attackContext.target == null)
        {
            CombatManager combatManager = FindAnyObjectByType<CombatManager>();
            for (int i = 0; i < 3; i++)
            {
                if (combatManager.GetEnemy(i).activeSelf)
                {
                    attackContext.target = combatManager.GetEnemy(i).GetComponent<Enemy>();
                }
            }
        }
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        bool successfulAttack = false;
        foreach (AttackSO attack in possibleAttacks)
        {
            if (attack.gestureName == gestureName && attack.colour == color)
            {
                attack.Attack(attackContext);
                successfulAttack = true;
            }
            
        }

        return successfulAttack;
    }

    public void SelectTarget(int targetIndex)
    {
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        attackContext.target = combatManager.GetEnemy(targetIndex).GetComponent<Enemy>();
        Debug.Log(attackContext.target);    
        Vector3 targetPosition = combatManager.GetEnemy(targetIndex).transform.position;
        targetPosition.z = -1;
        uiManager.targetReticle.transform.position = targetPosition;
        
    }

    public override float TakeDamage(AttackSO attack, AttackContext context)
    {
        float damageDealt = base.TakeDamage(attack, context);
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdatePlayerHealthBar(health);
        if (health <= 0)
        {
            CombatManager.LoseCombat();
        }
        return damageDealt;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // attackStat = playerLevel * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
