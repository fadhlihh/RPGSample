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
        }
    }
}
