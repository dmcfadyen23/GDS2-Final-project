using UnityEngine;

public class Enemy : Entity //Add enemy stuff here
{
    [SerializeField] private string enemyID;
    [SerializeField] private Attack basicAttack;
    public string weakness;
    public string resistance;
    

    public string GetEnemyID()
    {
        return enemyID;
    }

    public Attack GetAttack()
    {
        return basicAttack;
    }

    public Attack initiateAttack()
    {
        return basicAttack;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsEnemyDefeated(enemyID))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
