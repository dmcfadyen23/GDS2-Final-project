using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Player : Entity
{
    public List<AttackSO> possibleAttacks;

    private AttackContext attackContext = new AttackContext();

    private int playerLevel;

    public void UseAttack(string gestureName, Color color)
    {
        if (attackContext.target == null)
        {
            CombatManager combatManager = FindAnyObjectByType<CombatManager>();
            attackContext.target = combatManager.GetEnemy(0).GetComponent<Enemy>();
        }
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        foreach (AttackSO attack in possibleAttacks)
        {
            if (attack.gestureName == gestureName && attack.colour == color)
            {
                attack.Attack(attackContext);
            }
        }
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

    public override void TakeDamage(AttackSO attack)
    {
        base.TakeDamage(attack);
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.UpdatePlayerHealthBar(health);
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
