using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Fadhli.Game.Module
{
    [RequireComponent(typeof(Character))]
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField]
        private int _hitPoint = 30;
        [SerializeField]
        private int _maxCombo = 1;
        [SerializeField]
        private int _comboDuration = 3;
        [SerializeField]
        private Transform _swordSocket;
        [SerializeField]
        private Vector3 _hitboxSize = new Vector3(1, 1, 1);
        [SerializeField]
        private Vector3 _hitboxOffset = new Vector3(0, 0, 0);
        [SerializeField]
        private LayerMask _hitableLayer;

        private Character _character;
        private Coroutine _resetComboTimerCoroutine;
        private HashSet<Collider> _alreadyHit = new HashSet<Collider>();

        public UnityEvent<int> OnAttack;

        public int Combo { get; private set; } = 1;
        public bool IsAttacking { get; set; }
        public bool IsTracingHit { get; private set; }
        public HashSet<Collider> AlreadyHit { get { return _alreadyHit; } }

        public void StartTraceHit()
        {
            IsTracingHit = true;
            _alreadyHit.Clear();
        }

        public void StopTraceHit()
        {
            IsTracingHit = false;
            _alreadyHit.Clear();
        }

        private void Start()
        {
            _character = GetComponent<Character>();
        }

        private void OnEnable()
        {
            InputManager.Instance.OnLightAttackInput += Attack;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnLightAttackInput -= Attack;
        }

        private void Attack()
        {
            bool isRolling = GetComponent<IRolling>() != null ? GetComponent<IRolling>().IsRolling : false;
            if (!IsAttacking && _character.CharacterMovement.IsGrounded && !isRolling)
            {
                IsAttacking = true;
                OnAttack?.Invoke(Combo);
                CountCombo();
            }
        }

        private void CountCombo()
        {
            Combo = Combo >= _maxCombo ? 1 : Combo + 1;
            if (_resetComboTimerCoroutine != null)
            {
                StopCoroutine(_resetComboTimerCoroutine);
            }
            _resetComboTimerCoroutine = StartCoroutine(ResetComboTimerHandler());
        }

        private IEnumerator ResetComboTimerHandler()
        {
            yield return new WaitForSeconds(_comboDuration);
            Combo = 1;
        }

        private void Update()
        {
            if (IsTracingHit)
            {
                Vector3 center = _swordSocket.position + _swordSocket.rotation * _hitboxOffset;
                Collider[] hits = Physics.OverlapBox(center, _hitboxSize / 2, _swordSocket.rotation, _hitableLayer);
                foreach (Collider hit in hits)
                {
                    if (!_alreadyHit.Contains(hit))
                    {
                        _alreadyHit.Add(hit);
                        hit.GetComponent<IDamagable>().Damage(_hitPoint);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsTracingHit ? Color.red : Color.white;
            Vector3 center = _swordSocket.position + _swordSocket.rotation * _hitboxOffset;
            Gizmos.matrix = Matrix4x4.TRS(center, _swordSocket.rotation, Vector3.one);
            Collider[] hits = Physics.OverlapBox(center, _hitboxSize / 2, _swordSocket.rotation, _hitableLayer);
            if (hits.Length > 0)
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawWireCube(Vector3.zero, _hitboxSize);
        }
    }
}
