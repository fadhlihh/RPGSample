using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int HealthPoint { get; private set; } = 100;
    public bool IsDead { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        if (!_characterDefense)
        {
            _characterDefense = GetComponent<CharacterDefense>();
        }
    }

    public void Damage(Character instigator, int hitPoint)
    {
        Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        PlayerCharacter playerCharacter = instigator as PlayerCharacter;
        if (!IsDead)
        {
            HealthPoint -= (hitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
            EnemyUI.SetHealthBarValue(HealthPoint);
            if (!CharacterDefense.IsBlocking)
            {
                OnDamage?.Invoke();
            }
            else
            {
                playerCharacter.BounceBack();
                SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitBlock, 0.5f, 1);
            }
            if (HealthPoint <= 0)
            {
                Death();
            }
        }
    }

    public void Damage(Character instigator, int hitPoint, Vector3 hitImpact)
    {
        Instantiate(_impactPrefab, hitImpact, Quaternion.identity);
        PlayerCharacter playerCharacter = instigator as PlayerCharacter;
        if (!IsDead)
        {
            HealthPoint -= (hitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
            EnemyUI.SetHealthBarValue(HealthPoint);
            if (!CharacterDefense.IsBlocking)
            {
                OnDamage?.Invoke();
            }
            else
            {
                playerCharacter.BounceBack();
                SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitBlock, 0.5f, 1);
            }
            if (HealthPoint <= 0)
            {
                Death();
            }
        }
    }

    public void Damage(int hitPoint, Vector3 hitImpact)
    {
        Instantiate(_impactPrefab, hitImpact, Quaternion.identity);
        if (!IsDead)
        {
            HealthPoint -= (hitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
            EnemyUI.SetHealthBarValue(HealthPoint);
            if (!CharacterDefense.IsBlocking)
            {
                OnDamage?.Invoke();
            }
            else
            {
                SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.SwordHitBlock, 0.5f, 1);
            }
            if (HealthPoint <= 0)
            {
                Death();
            }
        }
    }

    public void Damage(int hitPoint)
    {
        Instantiate(_impactPrefab, transform.position, Quaternion.identity);
        if (!IsDead)
        {
            HealthPoint -= (hitPoint + (CharacterDefense.IsBlocking ? CharacterDefense.DamageModifier : 0));
            EnemyUI.SetHealthBarValue(HealthPoint);
            if (!CharacterDefense.IsBlocking)
            {
                OnDamage?.Invoke();
            }
            else
            {
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

    }
}
