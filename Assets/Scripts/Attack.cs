using UnityEngine;
using System;

[Serializable]
public class Attack
{
    public string attackName;
    public int damage;

    public Attack(string attackName, int damage)
    {
        this.attackName = attackName;
        this.damage = damage;
    }
}
