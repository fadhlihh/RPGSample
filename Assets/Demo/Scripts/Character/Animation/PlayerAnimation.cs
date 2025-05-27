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
        public UnityEvent OnBeginHeavyAttackingAnimation;
        public UnityEvent OnEndHeavyAttackingAnimation;

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

        public void OnBeginLockTarget()
        {
            _animator.SetBool("IsLockTarget", true);
        }

        public void OnEndLockTarget()
        {
            _animator.SetBool("IsLockTarget", false);
        }

        protected void Update()
        {
            base.Update();
            if (_ownerCharacter.CharacterMovement != null)
            {
                ControllerCharacterMovement controllerMovement = _ownerCharacter.CharacterMovement as ControllerCharacterMovement;
                _animator.SetFloat("Velocity", controllerMovement.NormalizedVelocity.magnitude);
                _animator.SetFloat("VelocityX", controllerMovement.NormalizedVelocityXZ.x);
                _animator.SetFloat("VelocityZ", controllerMovement.NormalizedVelocityXZ.z);
            }
        }
    }
}
