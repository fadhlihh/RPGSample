using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Fadhli.Game.Module
{
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
            Destroy(gameObject);
        }
    }
}
