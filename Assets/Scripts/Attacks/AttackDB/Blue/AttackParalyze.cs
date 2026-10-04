using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackParalyze", menuName = "Scriptable Objects/AttackParalyze")]
    public class AttackParalyze : AttackSO
    {
        public AttackParalyze()
        {
            attackName = "Paralyze";
            basePower = 0;
            gestureName = "LightningBolt";
            targetingType = TargetingType.SINGLE_TARGET;
        }
        
        public override void Attack(AttackContext context)
        {
            uiManager = FindAnyObjectByType<UIManager>();
            Entity target = context.target;
            target.status.Add(("Paralyze", -1));
            context.animator.Play("AttackAnim");
            uiManager.battleLogText.text = context.sourceUnit.unitName + " used " + attackName + ", the enemy may be unable to move!";
            
            EndOfTurn(context);
        }
    }
}