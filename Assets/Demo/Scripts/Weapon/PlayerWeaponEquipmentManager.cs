using System.Collections;
using System.Collections.Generic;
using Fadhli.Game.Module;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Character), typeof(CharacterAnimation))]
public class PlayerWeaponEquipmentManager : WeaponEquipmentManager
{
    [SerializeField]
    private Transform _swordSocket;
    [SerializeField]
    private Transform _bowSocket;
    [SerializeField]
    private Transform _spellSocket;

    public UnityEvent OnStartAim;
    public UnityEvent OnStopAim;
    public UnityEvent<EWeaponType> OnSwitchWeapon;

    public bool IsAiming { get; private set; }

    protected override void InitSocket()
    {
        if (_swordSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Melee, _swordSocket);
        }

        if (_bowSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Range, _bowSocket);
        }

        if (_spellSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Spell, _spellSocket);
        }
    }

    public void NextWeapon()
    {
        if (!IsAttacking && !IsHeavyAttack)
        {
            _weaponSockets[_playerWeapons[_currentWeaponIndex].Type].gameObject.SetActive(false);
            _animation.OnBeginTraceHitAnimation.RemoveListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
            _animation.OnEndTraceHitAnimation.RemoveListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
            if (_currentWeaponIndex < _playerWeapons.Count - 1)
            {
                _currentWeaponIndex += 1;
            }
            else
            {
                _currentWeaponIndex = 0;
            }
            _animation.OnBeginTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
            _animation.OnEndTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
            HUDManager.Instance.WeaponSlotUI.SetIcon(_playerWeapons[_currentWeaponIndex].Icon);
            _weaponSockets[_playerWeapons[_currentWeaponIndex].Type].gameObject.SetActive(true);
            OnSwitchWeapon?.Invoke(_playerWeapons[_currentWeaponIndex].Type);
        }
    }

    public override void LightAttack()
    {
        bool isRolling = GetComponent<IRolling>() != null ? GetComponent<IRolling>().IsRolling : false;
        bool isGrounded = GetComponent<CharacterMovement>() != null ? GetComponent<CharacterMovement>().IsGrounded : true;
        IStamina staminaSystem = GetComponent<IStamina>();
        bool isStaminaAvailable = staminaSystem != null ? GetComponent<IStamina>().GetIsStaminaAvailable(20) : true;
        if (!IsAttacking && isGrounded && !isRolling && isStaminaAvailable && !IsAiming)
        {
            IsAttacking = true;
            _playerWeapons[_currentWeaponIndex].LightAttack();
            OnAttack.Invoke(_playerWeapons[_currentWeaponIndex].Combo);
            staminaSystem?.DecreaseStamina(20);
        }
    }

    public void StartAim()
    {
        if (PlayerWeapons[_currentWeaponIndex].CanAim && !IsAttacking)
        {
            IsAiming = true;
            OnStartAim?.Invoke();
            PlayerWeapons[_currentWeaponIndex].StartAim();
            HUDManager.Instance.CharacterUI.ShowAimCrosshair();
        }
    }

    public void StopAim()
    {
        if (IsAiming)
        {
            IsAiming = false;
            OnStopAim?.Invoke();
            PlayerWeapons[_currentWeaponIndex].StopAim();
            HUDManager.Instance.CharacterUI.HideAimCrosshair();
        }
    }
}
