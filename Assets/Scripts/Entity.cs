using System.Collections.Generic;
using Attacks;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public string unitName;
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public int accuracy = 100;
    public float attackStat;
    public List<(string name, int duration)> buffs = new List<(string name, int duration)>();
    public List<(string name, int duration)> debuffs = new List<(string name, int duration)>();
    public List<(string name, int duration)> statuses = new List<(string name, int duration)>();


    public virtual float TakeDamage(AttackSO attack, AttackContext context)
    {
        health -= attack.GetDamageDealt();
        return attack.GetDamageDealt();
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
