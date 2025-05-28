using Fadhli.Game.Module;
using UnityEngine;
using UnityEngine.Events;

public class CombatAnimation : CharacterAnimation
{
    public UnityEvent OnBeginAttackingAnimation;
    public UnityEvent OnEndAttackingAnimation;
    public UnityEvent OnBeginTraceHitAnimation;
    public UnityEvent OnEndTraceHitAnimation;
    public UnityEvent OnBeginHeavyAttackingAnimation;
    public UnityEvent OnEndHeavyAttackingAnimation;
    public UnityEvent OnBeginBlockAnimation;
    public UnityEvent OnEndBlockAnimation;

    public void OnCharacterBeginAttack(int combo)
    {
        _animator.applyRootMotion = true;
        _animator.SetBool("IsAttacking", true);
        _animator.SetInteger("Combo", combo);
        OnBeginAttackingAnimation?.Invoke();
    }

    public void OnCharacterEndAttack()
    {
        _animator.SetBool("IsAttacking", false);
        _animator.applyRootMotion = false;
        OnEndAttackingAnimation?.Invoke();
    }

    public void OnCharacterBeginHeavyAttack(EWeaponType type)
    {
        _animator.applyRootMotion = true;
        _animator.SetBool("IsHeavyAttack", true);
        _animator.SetBool("IsUsingBow", (type == EWeaponType.Range));
        _animator.SetBool("IsUsingSpell", (type == EWeaponType.Spell));
        _animator.SetBool("IsAttacking", true);
        OnBeginHeavyAttackingAnimation?.Invoke();
    }

    public void OnCharacterEndHeavyAttack()
    {
        _animator.SetBool("IsAttacking", false);
        _animator.SetBool("IsHeavyAttack", false);
        _animator.SetBool("IsUsingBow", false);
        _animator.SetBool("IsUsingSpell", false);
        _animator.applyRootMotion = false;
        OnEndHeavyAttackingAnimation?.Invoke();
    }

    public void OnStartTracingHit()
    {
        OnBeginTraceHitAnimation?.Invoke();
    }

    public void OnEndTracingHit()
    {
        OnEndTraceHitAnimation?.Invoke();
    }

    public void OnCharacterBeginBlock()
    {
        _animator.SetLayerWeight(1, 1);
        OnBeginBlockAnimation?.Invoke();
    }

    public void OnCharacterStopBlock()
    {
        _animator.SetLayerWeight(1, 0);
        OnEndBlockAnimation?.Invoke();
    }

    public void OnCharacterBeginHit()
    {
        _animator.Play("Hit_In_Place");
    }
}
