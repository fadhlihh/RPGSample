using UnityEngine;

public class ProjectileSpell : MonoBehaviour
{
    [SerializeField]
    private float _lifespan;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _speed;
    private bool _isLaunched;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float speed, int damage)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
        _isLaunched = true;
        Destroy(gameObject, _lifespan);
    }

    private void Update()
    {
        if (_isLaunched)
        {
            _rigidbody.linearVelocity = _direction.normalized * _speed * Time.deltaTime;
            transform.forward = _direction;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(_explosionPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(new DamageData(null, _damage, other.ClosestPoint(transform.position)));
        }
        Destroy(gameObject);
    }
}
