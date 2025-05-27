using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{

    public override void HeavyAttack()
    {
        _damageModifier = 10;
    }
}
