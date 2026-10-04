using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public string unitName;
    public float health = 100.0f;
    public int accuracy = 100;
    public float attackStat;
    public List<(string name, int duration)> buff = new List<(string name, int duration)>();
    public List<(string name, int duration)> debuff = new List<(string name, int duration)>();
    public List<(string name, int duration)> status = new List<(string name, int duration)>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
