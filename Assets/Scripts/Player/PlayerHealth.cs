using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 100;

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
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
