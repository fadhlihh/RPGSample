using Fadhli.Game.Module;
using UnityEngine;
using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    [RequireComponent(typeof(Character), typeof(CharacterController))]
    public class PlayerAnimation : CharacterAnimation
    {
        public UnityEvent OnBeginRollingAnimation;
        public UnityEvent OnEndRollingAnimation;
        public UnityEvent OnBeginAttackingAnimation;
        public UnityEvent OnEndAttackingAnimation;
        public UnityEvent OnBeginTraceHitAnimation;
        public UnityEvent OnEndTraceHitAnimation;

        public void OnCharacterBeginRoll()
        {
            _animator.applyRootMotion = true;
            _animator.SetBool("IsRolling", true);
            OnBeginRollingAnimation?.Invoke();
        }

        public void OnCharacterEndRoll()
        {
            _animator.SetBool("IsRolling", false);
            _animator.applyRootMotion = false;
            OnEndRollingAnimation?.Invoke();
        }

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

        public void OnStartTracingHit()
        {
            OnBeginTraceHitAnimation?.Invoke();
        }

        public void OnEndTracingHit()
        {
            OnEndTraceHitAnimation?.Invoke();
        }
    }
}
