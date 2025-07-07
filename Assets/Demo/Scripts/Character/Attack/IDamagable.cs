using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public int HealthPoint { get; }
    public bool IsDead { get; }

    public void Damage(Character instigator, int hitPoint, Vector3 hitImpact);
    public void Damage(Character instigator, int hitPoint);
    public void Damage(int hitPoint, Vector3 hitImpact);
    public void Damage(int hitPoint);
    public void Heal(int value);

    public void Death();
}

