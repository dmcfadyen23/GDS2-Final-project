using Attacks;
using UnityEngine;

namespace Attacks.AttackDB
{
    [CreateAssetMenu(fileName = "AttackLightning", menuName = "Scriptable Objects/AttackLightning")]
    public class AttackLightning : AttackSO
    {
        public AttackLightning()
        {
            attackName = "Lightning Bolt";
            basePower = 50;
            gestureName = "LightningBolt";
            targetingType = TargetingType.SINGLE_TARGET;
        }
    }
}