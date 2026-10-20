using Attacks;
using UnityEngine;

public class Enemy : Entity //Add enemy stuff here
{
    [SerializeField] public string enemyID;
    [SerializeField] public AttackSO basicAttack;
    public string weakness;
    public string resistance;
    public string immune;
    private AttackContext attackContext = new AttackContext();
    

    public string GetEnemyID()
    {
        return enemyID;
    }

    public AttackSO GetAttack()
    {
        return basicAttack;
    }

    public override void TakeDamage(AttackSO attack)
    {
        float damageTaken = attack.GetDamageDealt();
        if (attack.attackName == weakness)
        {
            damageTaken *= 2;
        }
        else if (attack.attackName == resistance)
        {
            damageTaken *= 0.5f;
        }
        else if (attack.attackName == immune)
        {
            damageTaken = 0;
        }

        health -= damageTaken;
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
