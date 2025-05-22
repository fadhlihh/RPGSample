using Fadhli.Game.Module;
using UnityEngine;

namespace Fadhli
{
    [RequireComponent(typeof(Character))]
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField]
        protected Animator _animator;

        protected Character _ownerCharacter;

        protected void Awake()
        {
            if (!_animator)
            {
                _animator = GetComponentInChildren<Animator>();
                if (!_animator)
                {
                    _animator = GetComponent<Animator>();
                }
            }
        }
        protected void Start()
        {
            _ownerCharacter = GetComponent<Character>();
        }

        protected void Update()
        {
            if (_ownerCharacter.CharacterMovement != null)
            {
                _animator.SetFloat("Velocity", _ownerCharacter.CharacterMovement.VelocityXZ.magnitude);
                _animator.SetFloat("VelocityY", _ownerCharacter.CharacterMovement.VelocityY);
                _animator.SetBool("IsFalling", _ownerCharacter.CharacterMovement.IsFalling);
            }
        }
    }
}
