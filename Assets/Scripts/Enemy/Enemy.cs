using Attacks;
using UnityEngine;

public class Enemy : Entity //Add enemy stuff here
{
    [SerializeField] private string enemyID;
    [SerializeField] private AttackSO basicAttack;
    public string weakness;
    public string resistance;
    private AttackContext attackContext = new AttackContext();
    

    public string GetEnemyID()
    {
        return enemyID;
    }

    public AttackSO GetAttack()
    {
        return basicAttack;
    }

    public void UseAttack(Player targetPlayer)
    {
        attackContext.target = targetPlayer;
        attackContext.sourceUnit = this;
        attackContext.animator = GetComponentInChildren<Animator>();
        basicAttack.Attack(attackContext);
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
