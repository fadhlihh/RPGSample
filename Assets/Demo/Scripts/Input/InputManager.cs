using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static IA_Default;

public class InputManager : Singleton<InputManager>, IGeneralActions
{
    public Action<Vector2> OnMoveInput;
    public Action OnRollInput;
    public Action<bool> OnSprintInput;
    public Action OnLightAttackInput;
    public Action OnHeavyAttackInput;
    public Action OnLockTargetInput;
    public Action OnSwitchWeaponInput;
    public Action OnStartBlockInput;
    public Action OnStopBlockInput;
    public Action OnParryInput;
    public Action OnUseItemInput;
    public Action OnStartAimInput;
    public Action OnStopAimInput;

    private IA_Default _inputAction;
    public InputManager()
    {
        if (_inputAction == null)
        {
            _inputAction = new IA_Default();
            _inputAction.General.SetCallbacks(this);
        }
    }

    public void SetGeneralInputEnabled(bool value)
    {
        if (value)
        {
            _inputAction.General.Enable();
        }
        else
        {
            _inputAction.General.Disable();
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSprintInput?.Invoke(true);
        }
        if (context.canceled)
        {
            OnSprintInput?.Invoke(false);
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRollInput?.Invoke();
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnLightAttackInput?.Invoke();
        }
    }

    public void OnLockTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnLockTargetInput?.Invoke();
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnHeavyAttackInput?.Invoke();
        }
    }

    public void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSwitchWeaponInput?.Invoke();
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnStartBlockInput?.Invoke();
        }
        if (context.canceled)
        {
            OnStopBlockInput?.Invoke();
        }
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnParryInput?.Invoke();
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnUseItemInput?.Invoke();
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnStartAimInput?.Invoke();
        }
        if (context.canceled)
        {
            OnStopAimInput?.Invoke();
        }
    }
}
