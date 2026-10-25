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
            Entity target;
            switch (targetingType)
            {
                case TargetingType.SELF:
                    target = context.sourceUnit;
                    break;
                case TargetingType.SINGLE_TARGET:
                    target = context.target;
                    break;
                default:
                    target = context.sourceUnit;
                    break;
            }
            float damageHealed = healAmount*(target.attackStat/100);
            if (target as Chef)
            {
                damageHealed = 0;
                Chef enemyChef = (Chef)target;
                enemyChef.customDialogue.text = "Oh no you don't! Only the Chef's food can heal you here!";
            }

            target.health += damageHealed;
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = target.unitName + " was healed using " + attackName + " for " + damageHealed + " health";
            if (target as Enemy)
            {
                Enemy enemyTarget = (Enemy)target;
                uiManager.UpdateEnemyHealthBar(target.health, enemyTarget.GetEnemyNum());
            }
            else
            {
                uiManager.UpdatePlayerHealthBar(target.health);
            }
            EndOfTurn(context);
        }
    }
}