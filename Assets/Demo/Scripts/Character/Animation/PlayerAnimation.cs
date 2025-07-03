using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    public class PlayerAnimation : CombatAnimation
    {
        public UnityEvent OnBeginRollingAnimation;
        public UnityEvent OnEndRollingAnimation;
        public UnityEvent OnBeginParryAnimation;
        public UnityEvent OnEndParryAnimation;

        public void PlayRollSFX()
        {
            SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.Roll, 0.75f, 1);
        }

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

        public void OnBeginLockTarget()
        {
            _animator.SetBool("IsLockTarget", true);
        }

        public void OnEndLockTarget()
        {
            _animator.SetBool("IsLockTarget", false);
        }

        public void OnCharacterParry()
        {
            _animator.Play("Parry");
            OnCharacterEndAttack();
            OnCharacterEndHeavyAttack();
            OnBeginParryAnimation?.Invoke();
        }

        public void OnCharacterEndParry()
        {
            OnEndParryAnimation?.Invoke();
        }

        public void OnCharacterBeginAim()
        {
            _animator.SetBool("IsAiming", true);
        }

        public void OnCharacterEndAim()
        {
            _animator.SetBool("IsAiming", false);
        }

        protected override void Update()
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
