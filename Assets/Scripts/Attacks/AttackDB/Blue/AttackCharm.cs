using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackCharm", menuName = "Scriptable Objects/AttackCharm")]
    public class AttackCharm : AttackSO
    {
        public AttackCharm()
        {
            attackName = "Charm";
            basePower = 0;
            gestureName = "Heart";
            targetingType = TargetingType.AOE;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Enemy[] enemies = FindObjectsByType<Enemy>();
            int activeEnemies = 0;
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.activeSelf)
                {
                    enemy.debuffs.Add(("Charmed", 3));
                    activeEnemies++;
                }
            }

            if (activeEnemies > 1)
            {
                uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", reducing all enemies' damage!";
            }
            else
            {
                uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", reducing the enemy's damage!";
            }
            
            context.animator.Play("AttackAnim");
            EndOfTurn(context);
        }
    }
}