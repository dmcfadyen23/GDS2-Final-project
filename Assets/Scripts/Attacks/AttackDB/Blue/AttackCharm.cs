using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackCharm", menuName = "Scriptable Objects/AttackCharm")]
    public class AttackCharm : AttackSO
    {
        public AttackCharm()
        {
            attackName = "Charm";
            basePower = 0;
            gestureName = "Heart";
            targetingType = TargetingType.AOE;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.target;
            target.debuff.Add(("Charmed", 3));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", reducing the enemy's damage!";
            
            EndOfTurn(context);
        }
    }
}