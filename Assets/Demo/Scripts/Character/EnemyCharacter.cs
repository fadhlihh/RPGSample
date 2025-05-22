using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fadhli.Game.Module
{
    public class EnemyCharacter : Character, IDamagable
    {
        public int HealthPoint { get; private set; } = 100;
        public bool IsDead { get; private set; } = false;

        public void Damage(int hitPoint)
        {
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
