using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : CombatAnimation
{
    public void OnCharacterKnockback()
    {
        _animator.Play("Parry Reaction");
        OnCharacterEndAttack();
        OnCharacterEndHeavyAttack();
    }
}
