using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override EWeaponType Type => EWeaponType.Melee;

    public override void HeavyAttack()
    {
        _damageModifier = 10;
    }
}
