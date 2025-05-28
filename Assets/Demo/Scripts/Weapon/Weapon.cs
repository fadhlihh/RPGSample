using System.Collections;
using System.Collections.Generic;
using Fadhli.Game.Module;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected int _damage;
    [SerializeField]
    private int _maxCombo = 1;
    [SerializeField]
    private int _comboDuration = 3;
    [SerializeField]
    private Transform _socket;
    [SerializeField]
    private Vector3 _hitboxSize = new Vector3(1, 1, 1);
    [SerializeField]
    private Vector3 _hitboxOffset = new Vector3(0, 0, 0);
    [SerializeField]
    private LayerMask _hitableLayer;
    private Coroutine _resetComboTimerCoroutine;
    private HashSet<Collider> _alreadyHit = new HashSet<Collider>();
    protected int _damageModifier;

    public virtual EWeaponType Type { get; }
    public string Name { get => _name; }
    public Sprite Icon { get => _icon; }
    public int Damage { get => _damage; }
    public int Combo { get; private set; } = 1;
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

    public void ResetDamageModifier()
    {
        _damageModifier = 0;
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
            Vector3 center = _socket.position + _socket.rotation * _hitboxOffset;
            Collider[] hits = Physics.OverlapBox(center, _hitboxSize / 2, _socket.rotation, _hitableLayer);
            foreach (Collider hit in hits)
            {
                if (!_alreadyHit.Contains(hit))
                {
                    _alreadyHit.Add(hit);
                    hit.GetComponent<IDamagable>().Damage(_damage + _damageModifier, hit.ClosestPoint(center));
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsTracingHit ? Color.red : Color.white;
        Vector3 center = _socket.position + _socket.rotation * _hitboxOffset;
        Gizmos.matrix = Matrix4x4.TRS(center, _socket.rotation, Vector3.one);
        Collider[] hits = Physics.OverlapBox(center, _hitboxSize / 2, _socket.rotation, _hitableLayer);
        if (hits.Length > 0)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(Vector3.zero, _hitboxSize);
    }

    public virtual void LightAttack()
    {
        CountCombo();
    }

    public abstract void HeavyAttack();
}
