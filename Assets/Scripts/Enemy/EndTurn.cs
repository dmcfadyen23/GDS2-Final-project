using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTurn : MonoBehaviour
{

    public void FinishTurn()
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

    //unit num, 0 = player, 1-3 is enemy. i.e. end of player's turn calls EndOfTurn(0)
    public void EndOfTurn(int unitNum)
    {
        Debug.Log(gameObject.name + "end turn");
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.WaitForEnemy();
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        // in array, enemies are 0,1,2. end of player's turn (0) starts array search from first enemy
        GameObject[] activeEnemies = combatManager.GetAllEnemies();
        Player player = FindAnyObjectByType<Player>();
        for (int i = unitNum; i < activeEnemies.Length; i++)
        {
            if (activeEnemies[i].activeSelf)
            {
                Enemy enemy = activeEnemies[i].GetComponent<Enemy>();
                enemy.UseAttack(player);
                Debug.Log(activeEnemies[i].name + "use attack");
                return;
            }
        }
        // if no enemies take turn (dead/not exist/last enemy took turn), becomes player turn
        FinishTurn();
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
