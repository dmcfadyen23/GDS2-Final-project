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
            basePower = 0;
            gestureName = "Heart";
            targetingType = TargetingType.SELF;
        }

        private int healAmount = 50;
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            float damageHealed = healAmount*(target.attackStat/100);

            target.health += damageHealed;
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " healed self using" + attackName + " for " + damageHealed + " health";
            if (target as Enemy)
            {
                uiManager.UpdateEnemyHealthBar(target.health);
            }
            else
            {
                uiManager.UpdatePlayerHealthBar(target.health);
            }
            EndOfTurn(context);
        }
    }
}