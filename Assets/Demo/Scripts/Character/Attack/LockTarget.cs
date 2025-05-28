using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    public class LockTarget : MonoBehaviour
    {
        [SerializeField]
        private float _maxLockDistance = 2f;
        [SerializeField]
        private CinemachineCamera _lockCamera;
        [SerializeField]
        private CinemachineOrbitalFollow _thirdPersonCamera;
        [SerializeField]
        private float _rotationSpeed = 1f;

        public float MaxLockDistance { get { return _maxLockDistance; } }
        public bool IsLockTarget { get; private set; }
        public EnemyCharacter Target { get; private set; }

        public UnityEvent OnStartLockTarget;
        public UnityEvent OnStopLockTarget;

        private void OnEnable()
        {
            InputManager.Instance.OnLockTargetInput += OnLockTargetInput;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnLockTargetInput -= OnLockTargetInput;
        }

        public void OnLockTargetInput()
        {
            if (!IsLockTarget)
            {
                EnemyCharacter closestEnemy = GetClosestEnemy();
                if (closestEnemy != null)
                {
                    StartLockTarget(closestEnemy);
                }
            }
            else
            {
                StopLockTarget();
            }
        }

        public void StartLockTarget(EnemyCharacter enemy)
        {
            IsLockTarget = true;
            Target = enemy;
            _lockCamera.Priority.Value = 2;
            enemy.OnDeath.AddListener(StopLockTarget);
            OnStartLockTarget?.Invoke();
        }

        public void StopLockTarget()
        {
            IsLockTarget = false;
            OnStopLockTarget?.Invoke();
            Target = null;
            _thirdPersonCamera.HorizontalAxis.Value = transform.eulerAngles.y;
            _lockCamera.Priority.Value = 0;
        }

        private EnemyCharacter GetClosestEnemy()
        {
            if (EnemyManager.Instance.Enemies == null || EnemyManager.Instance.Enemies.Count <= 0)
            {
                return null;
            }

            EnemyCharacter closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (EnemyCharacter enemy in EnemyManager.Instance.Enemies)
            {
                if (enemy == null)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < MaxLockDistance && distance < closestDistance)
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }

        private bool IsTargetStillInReach()
        {
            float distance = Vector3.Distance(transform.position, Target.transform.position);
            return distance < MaxLockDistance;
        }

        private void Update()
        {
            if (IsLockTarget)
            {
                bool isRolling = GetComponent<IRolling>() != null ? GetComponent<IRolling>().IsRolling : false;
                if (isRolling)
                {
                    // transform.LookAt(Target.transform.position);
                    Vector3 toTarget = Target.transform.position - transform.position;
                    toTarget.y = 0;
                    if (toTarget.sqrMagnitude > 0.001f)
                    {
                        Quaternion lookRotation = Quaternion.LookRotation(toTarget);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
                    }
                }
                if (!IsTargetStillInReach())
                {
                    StopLockTarget();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, MaxLockDistance);
        }
    }
}
