using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackProtect", menuName = "Scriptable Objects/AttackProtect")]
    public class AttackProtect : AttackSO
    {
        public AttackProtect()
        {
            attackName = "Protect";
            basePower = 0;
            gestureName = "CircleWithCross";
            targetingType = TargetingType.SELF;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            bool alreadyProtected = false;
            foreach (var status in target.status)
            {
                if (status.name == "Protected")
                {
                    alreadyProtected = true;
                }
            }

            if (!alreadyProtected)
            {
                target.status.Add(("Protected", 2));
                context.animator.Play("AttackAnim");
                uiManager.battleLogText.text = context.sourceUnit.unitName + " protected self using" + attackName + " for this turn";
            }
            else
            {
                context.animator.Play("AttackAnim");
                uiManager.battleLogText.text = attackName + " failed!";
            }
            
            
            EndOfTurn(context);
        }
    }
}