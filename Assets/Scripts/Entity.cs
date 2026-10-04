using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    public string unitName;
    public float health = 100.0f;
    public int accuracy = 100;
    public float attackStat;
    public List<(string name, int duration)> buff;
    public List<(string name, int duration)> debuff;
    public List<string> status;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
