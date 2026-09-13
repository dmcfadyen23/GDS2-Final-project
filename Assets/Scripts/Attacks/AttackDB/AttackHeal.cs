using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackHeal", menuName = "Scriptable Objects/AttackHeal")]
    public class AttackHeal : AttackSO
    {
        public AttackHeal()
        {
            attackName = "Heal";
            basePower = -50;
            gestureName = "Heart";
            targetingType = TargetingType.SINGLE_TARGET;
        }
        
        public new void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            float damageDealt = basePower*(target.attackStat/100);
            if (target is Enemy)
            {
                Enemy enemyTarget = (Enemy)target;
                if (enemyTarget.weakness == attackName)
                {
                    damageDealt *= 2;
                }

                if (enemyTarget.resistance == attackName)
                {
                    damageDealt *= 0.5f;
                }
            }

            target.health += damageDealt;
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " healed self using" + attackName + " for " + damageDealt + " health";
            if (target as Enemy)
            {
                uiManager.UpdateEnemyHealthBar(target.health);
            }
            else
            {
                uiManager.UpdatePlayerHealthBar(target.health);
            }
        
        }
    }
}