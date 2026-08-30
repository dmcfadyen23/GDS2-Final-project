using UnityEngine;

public class Enemy : MonoBehaviour //Add enemy stuff here
{
    [SerializeField] private string enemyID;
    public int health = 100;

    public string GetEnemyID()
    {
        return enemyID;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
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
