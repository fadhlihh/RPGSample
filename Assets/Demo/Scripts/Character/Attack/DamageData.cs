using UnityEngine;

public struct DamageData
{
    public Character Instigator;
    public int HitPoint;
    public Vector3 HitImpactPosition;

    public DamageData(Character instigator, int hitPoint, Vector3 hitImpactPosition)
    {
        this.Instigator = instigator;
        this.HitPoint = hitPoint;
        this.HitImpactPosition = hitImpactPosition;
    }
}
