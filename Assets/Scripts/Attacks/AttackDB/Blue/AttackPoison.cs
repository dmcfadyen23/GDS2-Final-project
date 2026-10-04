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
            Entity target = context.target;
            target.debuff.Add(("Poison", 3));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", making the enemy take damage each turn!";
            
            EndOfTurn(context);
        }
    }
}