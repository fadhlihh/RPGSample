using UnityEngine;
using UnityEngine.Events;

public class EnemyCharacter : Character, IDamagable
{
    [SerializeField]
    private GameObject _impactPrefab;
    [SerializeField]
    private CharacterDefense _characterDefense;
    [SerializeField]
    private EnemyUI _enemyUI;

    public CharacterDefense CharacterDefense { get => _characterDefense; }
    public EnemyUI EnemyUI { get => _enemyUI; }
    public UnityEvent OnKnockback;
    public UnityEvent OnBounceback;

    public int HealthPoint { get; private set; }
    public int MaximumHealthPoint { get; private set; } = 100;
    public bool IsDead { get; private set; } = false;


    protected override void Awake()
    {
        base.Awake();
        HealthPoint = MaximumHealthPoint;
        if (!_characterDefense)
        {
            _characterDefense = GetComponent<CharacterDefense>();
        }
    }

    public void Damage(DamageData damageData)
    {
        Instantiate(_impactPrefab, damageData.HitImpactPosition, Quaternion.identity);
        PlayerCharacter playerCharacter = damageData.Instigator as PlayerCharacter;
        if (!IsDead)
        {
            HealthPoint -= (damageData.HitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
            EnemyUI.SetHealthBarValue(HealthPoint, MaximumHealthPoint);
            if (!CharacterDefense.IsBlocking)
            {
                OnDamage?.Invoke();
            }
            else
            {
                playerCharacter?.BounceBack();
                SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitBlock, 0.5f, 1);
            }
            if (HealthPoint <= 0)
            {
                Death();
            }
        }
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

    public void KnockBack()
    {
        OnKnockback?.Invoke();
    }

    public void BounceBack()
    {
        OnBounceback?.Invoke();
    }

    public void Heal(int value)
    {
        HealthPoint = HealthPoint + value;
        HealthPoint = Mathf.Clamp(HealthPoint, 0, 100);
        EnemyUI.SetHealthBarValue(HealthPoint, MaximumHealthPoint);
    }
}
