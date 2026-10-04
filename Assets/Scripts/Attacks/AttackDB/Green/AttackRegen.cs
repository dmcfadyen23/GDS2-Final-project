using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackRegen", menuName = "Scriptable Objects/AttackRegen")]
    public class AttackRegen : AttackSO
    {
        public AttackRegen()
        {
            attackName = "Regenerate";
            basePower = 10;
            gestureName = "Heart";
            targetingType = TargetingType.SELF;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            target.buff.Add(("Regen", 3));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " is now regenerating health each turn using" + attackName + " for " + 3 + " turns";
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