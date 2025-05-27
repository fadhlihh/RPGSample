using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fadhli.Game.Module
{
    public class EnemyCharacter : Character, IDamagable
    {
        [SerializeField]
        private GameObject _impactPrefab;

        public int HealthPoint { get; private set; } = 100;
        public bool IsDead { get; private set; } = false;

        public void Damage(int hitPoint)
        {
            Instantiate(_impactPrefab, transform.position, Quaternion.identity);
            if (!IsDead)
            {
                HealthPoint -= hitPoint;
                OnDamage?.Invoke();
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
                HealthPoint -= hitPoint;
                OnDamage?.Invoke();
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
