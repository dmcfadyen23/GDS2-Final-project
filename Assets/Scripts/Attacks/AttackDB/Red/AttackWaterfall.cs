using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackWaterfall", menuName = "Scriptable Objects/AttackWaterfall")]
    public class AttackWaterfall : AttackSO
    {
        public AttackWaterfall()
        {
            attackName = "Waterfall";
            basePower = 40;
            gestureName = "Teardrop";
            targetingType = TargetingType.SINGLE_TARGET;
        }
        
        public override void Attack(AttackContext context)
        {
            base.Attack(context);
            context.target.debuff.Add(("Doused", -1));
            uiManager.battleLogText.text = uiManager.battleLogText.text + "\n Enemy doused with water, weak to lightning!";
        }
    }
}