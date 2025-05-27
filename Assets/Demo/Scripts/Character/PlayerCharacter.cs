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
        private PlayerWeaponEquipmentManager _weaponEquipmentManager;
        [SerializeField]
        private UnityEvent _onCharacterRoll;

        public PlayerWeaponEquipmentManager WeaponEquipmentManager { get { return _weaponEquipmentManager; } }
        private bool _isFirstPerson;

        public bool IsFirstPerson { get { return _isFirstPerson; } set { _isFirstPerson = value; } }
        public DirectionalCharacterMovement DirectionalCharacterMovement { get; private set; }


        public UnityEvent OnCharacterRoll => _onCharacterRoll;

        public bool IsRolling { get; private set; }

        private void Awake()
        {
            base.Awake();
            DirectionalCharacterMovement = CharacterMovement as DirectionalCharacterMovement;
            if (!_weaponEquipmentManager)
            {
                _weaponEquipmentManager = GetComponent<PlayerWeaponEquipmentManager>();
            }
        }

        private void OnEnable()
        {
            InputManager.Instance.SetGeneralInputEnabled(true);
            InputManager.Instance.OnMoveInput += DirectionalCharacterMovement.AddMovementInput;
            InputManager.Instance.OnSprintInput += DirectionalCharacterMovement.Sprint;
            InputManager.Instance.OnRollInput += Roll;
            InputManager.Instance.OnLightAttackInput += WeaponEquipmentManager.LightAttack;
            InputManager.Instance.OnHeavyAttackInput += WeaponEquipmentManager.HeavyAttack;
            InputManager.Instance.OnSwitchWeaponInput += WeaponEquipmentManager.NextWeapon;
        }

        private void OnDisable()
        {
            InputManager.Instance.SetGeneralInputEnabled(false);
            InputManager.Instance.OnMoveInput -= DirectionalCharacterMovement.AddMovementInput;
            InputManager.Instance.OnSprintInput -= DirectionalCharacterMovement.Sprint;
            InputManager.Instance.OnRollInput -= Roll;
            InputManager.Instance.OnLightAttackInput -= WeaponEquipmentManager.LightAttack;
            InputManager.Instance.OnHeavyAttackInput -= WeaponEquipmentManager.HeavyAttack;
            InputManager.Instance.OnSwitchWeaponInput -= WeaponEquipmentManager.NextWeapon;
        }

        public void Roll()
        {
            if (DirectionalCharacterMovement.MoveDirection.magnitude > 0.01f && !WeaponEquipmentManager.IsAttacking)
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
