using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackWind", menuName = "Scriptable Objects/AttackWind")]
    public class AttackWind : AttackSO
    {
        public AttackWind()
        {
            attackName = "Wind Blast";
            basePower = 20;
            gestureName = "Spiral";
            targetingType = TargetingType.AOE;
        }
        
        public override void Attack(AttackContext context)
        {
            base.Attack(context);
            context.target.accuracy = 80;
            uiManager.battleLogText.text = uiManager.battleLogText.text + "\n Enemy accuracy reduced!";
        }
    }
}