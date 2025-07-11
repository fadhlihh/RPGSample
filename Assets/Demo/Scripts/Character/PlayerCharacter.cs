using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : Character, IRolling, IDamagable, IStamina
{
    [SerializeField]
    private PlayerWeaponEquipmentManager _weaponEquipmentManager;
    [SerializeField]
    private CharacterDefense _characterDefense;
    [SerializeField]
    private ItemManager _itemManager;
    [SerializeField]
    private GameObject _impactPrefab;
    [SerializeField]
    private UnityEvent _onCharacterRoll;
    [SerializeField]
    private int _staminaRegenSpeed = 1;

    private bool _isFirstPerson;

    public PlayerWeaponEquipmentManager WeaponEquipmentManager { get { return _weaponEquipmentManager; } }
    public bool IsFirstPerson { get { return _isFirstPerson; } set { _isFirstPerson = value; } }
    public DirectionalCharacterMovement DirectionalCharacterMovement { get; private set; }
    public CharacterDefense CharacterDefense { get => _characterDefense; }
    public ItemManager ItemManager { get => _itemManager; }


    public UnityEvent OnCharacterRoll => _onCharacterRoll;
    public UnityEvent OnBounceback;

    public bool IsRolling { get; private set; }

    public int MaximumHealthPoint { get; private set; } = 100;
    public int HealthPoint { get; private set; }

    public bool IsDead { get; private set; }
    public float MaximumStamina { get; private set; } = 100;
    public float Stamina { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        HealthPoint = MaximumHealthPoint;
        Stamina = MaximumStamina;
        DirectionalCharacterMovement = CharacterMovement as DirectionalCharacterMovement;
        if (!_weaponEquipmentManager)
        {
            _weaponEquipmentManager = GetComponent<PlayerWeaponEquipmentManager>();
        }

        if (!_characterDefense)
        {
            _characterDefense = GetComponent<CharacterDefense>();
        }
        if (!_itemManager)
        {
            _itemManager = GetComponent<ItemManager>();
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
        InputManager.Instance.OnStartBlockInput += CharacterDefense.StartBlock;
        InputManager.Instance.OnStopBlockInput += CharacterDefense.StopBlock;
        InputManager.Instance.OnParryInput += CharacterDefense.Parry;
        InputManager.Instance.OnUseItemInput += ItemManager.UseItem;
        InputManager.Instance.OnStartAimInput += WeaponEquipmentManager.StartAim;
        InputManager.Instance.OnStopAimInput += WeaponEquipmentManager.StopAim;
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
        InputManager.Instance.OnStartBlockInput -= CharacterDefense.StartBlock;
        InputManager.Instance.OnStopBlockInput -= CharacterDefense.StopBlock;
        InputManager.Instance.OnParryInput -= CharacterDefense.Parry;
        InputManager.Instance.OnUseItemInput -= ItemManager.UseItem;
        InputManager.Instance.OnStartAimInput -= WeaponEquipmentManager.StartAim;
        InputManager.Instance.OnStopAimInput -= WeaponEquipmentManager.StopAim;
    }

    public void Roll()
    {
        if (DirectionalCharacterMovement.MoveDirection.magnitude > 0.01f && !WeaponEquipmentManager.IsAttacking && GetIsStaminaAvailable(40) && !WeaponEquipmentManager.IsAiming)
        {
            DecreaseStamina(40);
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

    public void Damage(DamageData damageData)
    {
        EnemyCharacter enemyCharacter = damageData.Instigator as EnemyCharacter;
        if (!IsDead)
        {
            if (CharacterDefense.IsParrying)
            {
                enemyCharacter?.KnockBack();
                SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitParry, 0.5f, 1);
            }
            else
            {
                HealthPoint -= (damageData.HitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
                HUDManager.Instance.CharacterUI.SetHealthBarValue(HealthPoint, MaximumHealthPoint);
                if (!CharacterDefense.IsBlocking)
                {
                    OnDamage?.Invoke();
                }
                else
                {
                    enemyCharacter.BounceBack();
                    DecreaseStamina(30);
                    if (!GetIsStaminaAvailable(30))
                    {
                        CharacterDefense.StopBlock();
                    }
                    SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitBlock, 0.5f, 1);
                }
                if (HealthPoint <= 0)
                {
                    Death();
                }
            }
        }
        Instantiate(_impactPrefab, damageData.HitImpactPosition, Quaternion.identity);
    }

    public void Death()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }

    public void DestroyCharacter()
    {
        Destroy(gameObject, 3);
    }

    public void DecreaseStamina(float value)
    {
        Stamina -= value;
        Stamina = Mathf.Clamp(Stamina, 0, 100);
        HUDManager.Instance.CharacterUI.SetStaminaBarValue(Stamina);
    }

    public void IncreaseStamina(float value)
    {
        Stamina += value;
        Stamina = Mathf.Clamp(Stamina, 0, 100);
        HUDManager.Instance.CharacterUI.SetStaminaBarValue(Stamina);
    }

    public void Update()
    {
        if (!CharacterMovement.IsSprint)
        {
            IncreaseStamina(_staminaRegenSpeed * Time.deltaTime);
        }
    }

    public bool GetIsStaminaAvailable(int value)
    {
        return Stamina > value;
    }

    public void BounceBack()
    {
        OnBounceback?.Invoke();
    }

    public void Heal(int value)
    {
        HealthPoint = HealthPoint + value;
        HealthPoint = Mathf.Clamp(HealthPoint, 0, 100);
        HUDManager.Instance.CharacterUI.SetHealthBarValue(HealthPoint, MaximumHealthPoint);
    }

}
