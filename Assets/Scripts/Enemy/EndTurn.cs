using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTurn : MonoBehaviour
{

    public void FinishTurn(int i)
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.GoToMain();
        Player player = FindAnyObjectByType<Player>();
        if (player.health <= 0)
        {
            CombatManager.LoseCombat();
            SceneManager.LoadScene("TutorialFloor");
        }
    }

    public void EnemyTurn(int i)
    {
        
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.WaitForEnemy();
        Enemy enemy = FindAnyObjectByType<Enemy>();
        Player player = FindAnyObjectByType<Player>();
        if (enemy.health <= 0)
        {
            CombatManager.WinCombat();
        }
        enemy.UseAttack(player);
        
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
