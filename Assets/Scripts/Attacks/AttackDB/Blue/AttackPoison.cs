using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackPoison", menuName = "Scriptable Objects/AttackPoison")]
    public class AttackPoison : AttackSO
    {
        public AttackPoison()
        {
            attackName = "Poison";
            basePower = 0;
            gestureName = "CircleWithCross";
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
                    enemy.debuffs.Add(("Poison", 3));
                    activeEnemies++;
                }
            }
            if (activeEnemies > 1)
            {
                uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", making all enemies take damage each turn!";
            }
            else
            {
                uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", making the enemy take damage each turn!";
            }
            context.animator.Play("AttackAnim");
            
            EndOfTurn(context);
        }
    }
}