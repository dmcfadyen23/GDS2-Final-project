using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Player : Entity
{
    public List<AttackSO> possibleAttacks;

    private AttackContext attackContext;

    private int playerLevel;

    public void UseAttack(string gestureName)
    {
        Enemy targetEnemy = FindAnyObjectByType<Enemy>();
        attackContext.target = targetEnemy;
        attackContext.sourceUnit = this;
        foreach (AttackSO attack in possibleAttacks)
        {
            if (attack.gestureName == gestureName)
            {
                attack.Attack(attackContext);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackStat = playerLevel * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
