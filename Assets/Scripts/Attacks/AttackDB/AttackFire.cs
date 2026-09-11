using UnityEngine;

[CreateAssetMenu(fileName = "AttackFire", menuName = "Scriptable Objects/AttackFire")]
public class AttackFire : AttackSO
{
    public AttackFire()
    {
        attackName = "Fireball";
        gestureName = "CircleWithCross";
        basePower = 30;
        targetingType = TargetingType.BLAST;
    }
}
