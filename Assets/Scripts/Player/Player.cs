using System.Collections.Generic;
using Attacks;
using UnityEngine;

public class Player : Entity
{
    private List<AttackSO> possibleAttacks;

    private AttackContext attackContext;

    private int playerLevel;
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
