using System.Diagnostics;
using Fadhli.Game.Module;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    public class PlayerCharacter : Character, IRolling
    {
        [SerializeField]
        private CharacterAttack _characterAttack;
        [SerializeField]
        private UnityEvent _onCharacterRoll;

        private bool _isFirstPerson;

        public bool IsFirstPerson { get { return _isFirstPerson; } set { _isFirstPerson = value; } }
        public DirectionalCharacterMovement DirectionalCharacterMovement { get; private set; }

        public CharacterAttack CharacterAttack { get { return _characterAttack; } }

        public UnityEvent OnCharacterRoll => _onCharacterRoll;

        public bool IsRolling { get; private set; }

        private void Awake()
        {
            base.Awake();
            DirectionalCharacterMovement = CharacterMovement as DirectionalCharacterMovement;
            if (!_characterAttack)
            {
                _characterAttack = GetComponent<CharacterAttack>();
            }
        }

        private void OnEnable()
        {
            InputManager.Instance.SetGeneralInputEnabled(true);
            InputManager.Instance.OnMoveInput += DirectionalCharacterMovement.AddMovementInput;
            InputManager.Instance.OnSprintInput += DirectionalCharacterMovement.Sprint;
            InputManager.Instance.OnRollInput += Roll;
        }

        private void OnDisable()
        {
            InputManager.Instance.SetGeneralInputEnabled(false);
            InputManager.Instance.OnMoveInput -= DirectionalCharacterMovement.AddMovementInput;
            InputManager.Instance.OnSprintInput -= DirectionalCharacterMovement.Sprint;
            InputManager.Instance.OnRollInput -= Roll;
        }

        public void Roll()
        {
            bool isAttacking = GetComponent<CharacterAttack>() != null ? GetComponent<CharacterAttack>().IsAttacking : false;
            if (DirectionalCharacterMovement.MoveDirection.magnitude > 0.01f && !isAttacking)
            {
                DirectionalCharacterMovement.IsAbleToMove = false;
                IsRolling = true;
                Transform cameraTransform = Camera.main.transform;
                float rotationAngle = GameHelper.GetRotationAngleFromInput(DirectionalCharacterMovement.MoveDirection.x, DirectionalCharacterMovement.MoveDirection.z) + cameraTransform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
                OnCharacterRoll?.Invoke();
            }
        }

        public void EndRoll()
        {
            IsRolling = false;
            DirectionalCharacterMovement.IsAbleToMove = true;
        }
    }
}
