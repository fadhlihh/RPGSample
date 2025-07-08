using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent OnStartAim;
    public UnityEvent OnStopAim;

    private bool _isAiming;

    public float Distance { get => _distance; }
    public override EWeaponType Type => EWeaponType.Range;

    private void Start()
    {
        if (!_animator)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public override void StartTraceHit()
    {
        base.StartTraceHit();
        SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.Woosh, 0.5f, 1);
    }

    public override void HeavyAttack()
    {
        _animator.SetBool("IsFiring", true);
    }

    public void OnStartFiring()
    {
        if (_arrowProjectilePrefabs)
        {
            GameObject arrowObject = Instantiate(_arrowProjectilePrefabs, _arrowProjectileSpawner.position, _isAiming == true ? Quaternion.LookRotation(GetAimDirection()) : _arrowProjectileSpawner.rotation);
            ProjectileArrow projectileArrow = arrowObject.GetComponent<ProjectileArrow>();
            projectileArrow.Launch(_isAiming == true ? GetAimDirection() : arrowObject.transform.forward, _distance, 30);
            SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.ArrowRelease, 0.5f, 1);
        }
    }

    public void OnEndFiring()
    {
        _animator.SetBool("IsFiring", false);
    }

    public override void StartAim()
    {
        OnStartAim?.Invoke();
        _isAiming = true;
        _animator.SetBool("IsAiming", true);
    }

    public override void StopAim()
    {
        OnStopAim?.Invoke();
        _isAiming = false;
        _animator.SetBool("IsAiming", false);
    }

    private Vector3 GetAimDirection()
    {
        Camera camera = Camera.main;
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = camera.ScreenPointToRay(screenCenter);
        Vector3 targetPoint = ray.origin + ray.direction * 1000f;
        Vector3 direction = (targetPoint - _arrowProjectileSpawner.position).normalized;
        return direction.normalized;
    }
}
