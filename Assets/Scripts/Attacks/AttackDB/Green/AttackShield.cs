using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackShield", menuName = "Scriptable Objects/AttackShield")]
    public class AttackShield : AttackSO
    {
        public AttackShield()
        {
            attackName = "Shield";
            basePower = 0;
            gestureName = "Shield";
            targetingType = TargetingType.SELF;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.sourceUnit;
            target.buff.Add(("Shielded", 2));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " shielded self using" + attackName + ", taking less damage for 2 turns";
        }
    }
}