using UnityEngine;

public class Enemy : MonoBehaviour //Add enemy stuff here
{
    [SerializeField] private string enemyID;

    public string GetEnemyID()//Enemy Identifier
    {
        return enemyID;
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
