using System.Collections;
using System.Collections.Generic;
using Fadhli.Game.Module;
using UnityEngine;
using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    public abstract class ControllerCharacterMovement : CharacterMovement
    {
        [Header("Movement")]
        [SerializeField]
        protected float _jumpHeight = 1f;
        [SerializeField]
        protected float _gravityScale = 1f;
        protected CharacterController _characterController;

        public float JumpHeight { get { return _jumpHeight; } set { _jumpHeight = value; } }
        public float GravityScale { get { return _gravityScale; } set { _gravityScale = value; } }
        public bool IsCrouch { get; protected set; }
        public Vector3 MoveDirection { get; protected set; }
        public Vector3 NormalizedVelocity { get { return MoveDirection * _currentSpeed; } }
        public Vector3 NormalizedVelocityXZ { get { return new Vector3(NormalizedVelocity.x, 0, NormalizedVelocity.z); } }

        public UnityEvent OnCharacterRoll;

        public void AddMovementInput(Vector2 direction)
        {
            MoveDirection = new Vector3(direction.x, 0, direction.y);
        }

        public void AddMovementInput(float x, float y)
        {
            MoveDirection = new Vector3(x, 0, y);
        }

        public void Jump()
        {
            _velocityY = Mathf.Sqrt(_jumpHeight * 2f * -Physics.gravity.y);
        }

        public void Sprint(bool isSprint)
        {
            IsSprint = isSprint;
        }

        protected override void Update()
        {
            base.Update();
            IStamina staminaSystem = GetComponent<IStamina>();
            bool IsStaminaAvaliable = staminaSystem != null ? GetComponent<IStamina>().GetIsStaminaAvailable(5) : true;
            if (!IsStaminaAvaliable)
            {
                IsSprint = false;
            }
            if (IsStaminaAvaliable && IsSprint)
            {
                staminaSystem.DecreaseStamina(10 * Time.deltaTime);
            }
            CalculateAcceleration();
            ResetVelocityY();
            Move();
        }

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            GameHelper.SetHideAndLockCursor(true);
            _currentSpeed = _walkSpeed;
        }

        private void CalculateAcceleration()
        {
            if (MoveDirection.magnitude > 0.01f)
            {
                if (IsSprint)
                {
                    _currentSpeed = (_currentSpeed > _sprintSpeed) ? _sprintSpeed : _currentSpeed + _acceleration * Time.deltaTime;
                }
                else
                {
                    _currentSpeed = (_currentSpeed < _walkSpeed) ? _walkSpeed : _currentSpeed - _acceleration * Time.deltaTime;
                }
            }
            else
            {
                _currentSpeed = 0;
            }

        }

        private void ResetVelocityY()
        {
            if (IsGrounded && _velocityY < 0f)
            {
                _velocityY = -2;
            }
        }

        private void Move()
        {
            if (IsAbleToMove)
            {
                CalculateVelocityXZ();
                CalculateVelocityY();

                _velocityXZ = (MoveDirection.magnitude > 0.01) ? _velocityXZ * _currentSpeed : Vector3.zero;
                _velocity = _velocityXZ + (-Physics.gravity.normalized * _velocityY);

                _characterController.Move(_velocity * Time.deltaTime);
            }
        }

        private void CalculateVelocityY()
        {
            _velocityY += Physics.gravity.y * _gravityScale * Time.deltaTime;
        }

        protected abstract void RotateCharacter();
        protected abstract void CalculateVelocityXZ();

    }
}
