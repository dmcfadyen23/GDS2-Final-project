using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "FinalBossCharge", menuName = "Scriptable Objects/FinalBossCharge")]
    public class FinalBossCharge : AttackSO
    {
        public FinalBossCharge()
        {
            attackName = "Magic Charge";
            basePower = 0;
            gestureName = "Spiral";
            targetingType = TargetingType.SELF;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            target.buffs.Add(("MagicCharge", 2));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", buffing their offensive power!";
            
            EndOfTurn(context);
        }
    }
}