using Attacks;

namespace Attacks.AttackDB
{
    public class AttackLightning : AttackSO
    {
        public AttackLightning()
        {
            attackName = "LightningBolt";
            basePower = 50;
            targetingType = TargetingType.SINGLE_TARGET;
        }
    }
}