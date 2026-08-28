using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        GameManager.Instance.StartCombat(
            other.transform.position,
            enemy.GetEnemyID()
        );
    }
}
