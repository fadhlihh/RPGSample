using System.Collections;
using System.Collections.Generic;
using Fadhli.Game.Module;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Character), typeof(CharacterAnimation))]
public class PlayerWeaponEquipmentManager : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimation _playerAnimation;
    [SerializeField]
    private List<Weapon> _playerWeapons = new List<Weapon>();
    [SerializeField]
    private Transform _swordSocket;
    [SerializeField]
    private Transform _bowSocket;

    private Dictionary<EWeaponType, Transform> _weaponSockets = new Dictionary<EWeaponType, Transform>();
    private int _currentWeaponIndex = 0;
    private Character _character;

    public UnityEvent<int> OnAttack;
    public UnityEvent<EWeaponType> OnHeavyAttack;

    public List<Weapon> PlayerWeapons { get => _playerWeapons; }
    public bool IsAttacking { get; set; }
    public bool IsHeavyAttack { get; set; }

    public void InitSocket()
    {
        if (_swordSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Melee, _swordSocket);
        }

        if (_bowSocket != null)
        {
            _weaponSockets.Add(EWeaponType.Range, _bowSocket);
        }
    }

    private void Start()
    {
        _character = GetComponent<Character>();
        if (!_playerAnimation)
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }
        InitSocket();
        _playerAnimation.OnBeginTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
        _playerAnimation.OnEndTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
    }

    public void NextWeapon()
    {
        if (!IsAttacking && !IsHeavyAttack)
        {
            _weaponSockets[_playerWeapons[_currentWeaponIndex].Type].gameObject.SetActive(false);
            _playerAnimation.OnBeginTraceHitAnimation.RemoveListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
            _playerAnimation.OnEndTraceHitAnimation.RemoveListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
            if (_currentWeaponIndex < _playerWeapons.Count - 1)
            {
                _currentWeaponIndex += 1;
            }
            else
            {
                _currentWeaponIndex = 0;
            }
            _playerAnimation.OnBeginTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
            _playerAnimation.OnEndTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
            _weaponSockets[_playerWeapons[_currentWeaponIndex].Type].gameObject.SetActive(true);
        }
    }

    public void LightAttack()
    {
        bool isRolling = GetComponent<IRolling>() != null ? GetComponent<IRolling>().IsRolling : false;
        if (!IsAttacking && _character.CharacterMovement.IsGrounded && !isRolling)
        {
            IsAttacking = true;
            _playerWeapons[_currentWeaponIndex].LightAttack();
            OnAttack.Invoke(_playerWeapons[_currentWeaponIndex].Combo);
        }
    }

    public void HeavyAttack()
    {
        bool isRolling = GetComponent<IRolling>() != null ? GetComponent<IRolling>().IsRolling : false;
        if (!IsAttacking && _character.CharacterMovement.IsGrounded && !isRolling)
        {
            IsAttacking = true;
            IsHeavyAttack = true;
            _playerWeapons[_currentWeaponIndex].HeavyAttack();
            OnHeavyAttack.Invoke(_playerWeapons[_currentWeaponIndex].Type);
        }
    }

    public void EndHeavyAttack()
    {
        _playerWeapons[_currentWeaponIndex].ResetDamageModifier();
    }
}
