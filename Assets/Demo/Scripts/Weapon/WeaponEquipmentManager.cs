using System.Collections.Generic;
using Fadhli.Game.Module;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponEquipmentManager : MonoBehaviour
{
    [SerializeField]
    protected List<Weapon> _playerWeapons = new List<Weapon>();
    [SerializeField]
    protected CombatAnimation _animation;

    protected Dictionary<EWeaponType, Transform> _weaponSockets = new Dictionary<EWeaponType, Transform>();
    protected int _currentWeaponIndex = 0;
    protected Character _character;

    public List<Weapon> PlayerWeapons { get => _playerWeapons; }
    public bool IsAttacking { get; set; }
    public bool IsHeavyAttack { get; set; }

    public UnityEvent<int> OnAttack;
    public UnityEvent<EWeaponType> OnHeavyAttack;

    protected abstract void InitSocket();

    protected virtual void Start()
    {
        _character = GetComponent<Character>();
        if (!_animation)
        {
            _animation = GetComponent<PlayerAnimation>();
        }
        _animation.OnBeginTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StartTraceHit);
        _animation.OnEndTraceHitAnimation.AddListener(_playerWeapons[_currentWeaponIndex].StopTraceHit);
        InitSocket();
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
