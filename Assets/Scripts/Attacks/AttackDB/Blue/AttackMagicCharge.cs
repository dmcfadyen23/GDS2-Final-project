using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackMagicCharge", menuName = "Scriptable Objects/AttackMagicCharge")]
    public class AttackMagicCharge : AttackSO
    {
        public AttackMagicCharge()
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
            target.buff.Add(("MagicCharge", 4));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", buffing their offensive power!";
            
            EndOfTurn(context);
        }
    }
}