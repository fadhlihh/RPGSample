using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField]
    private float _distance;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _arrowProjectilePrefabs;
    [SerializeField]
    private Transform _arrowProjectileSpawner;

    public float Distance { get => _distance; }
    public override EWeaponType Type => EWeaponType.Range;

    private void Start()
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public override void HeavyAttack()
    {
        _animator.SetBool("IsFiring", true);
    }

    public void OnStartFiring()
    {
        if (_arrowProjectilePrefabs)
        {
            GameObject arrowObject = Instantiate(_arrowProjectilePrefabs, _arrowProjectileSpawner.position, _arrowProjectileSpawner.rotation);
            ProjectileArrow projectileArrow = arrowObject.GetComponent<ProjectileArrow>();
            projectileArrow.Launch(arrowObject.transform.forward, _distance, 30);
        }
    }

    public void OnEndFiring()
    {
        _animator.SetBool("IsFiring", false);
    }
}
