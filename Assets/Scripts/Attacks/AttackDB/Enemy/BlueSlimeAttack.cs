using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "BlueSlimeAttack", menuName = "Scriptable Objects/BlueSlimeAttack")]
    public class BlueSlimeAttack : AttackSO
    {
        public BlueSlimeAttack()
        {
            attackName = "Disarming Slime";
            basePower = 10;
            gestureName = "";
            targetingType = TargetingType.SINGLE_TARGET;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.target;
            target.debuffs.Add(("Poison", 3));
            float damageTaken = target.TakeDamage(this, context);
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", dealing " + damageTaken + " damage and making the enemy take damage each turn!";
            context.animator.Play("AttackAnim");
            
            EndOfTurn(context);
        }
    }
}