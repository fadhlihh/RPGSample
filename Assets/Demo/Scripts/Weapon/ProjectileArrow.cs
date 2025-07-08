using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileArrow : MonoBehaviour
{
    [SerializeField]
    private float _lifespan;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private int _damage;

    private void Awake()
    {
        if (!_rigidbody)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    public void Launch(Vector3 direction, float force, int damage)
    {
        _rigidbody.AddForce(direction.normalized * force);
        transform.forward = direction;
        _damage = damage;
        Destroy(gameObject, _lifespan);
    }

    void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(new DamageData(null, _damage, other.ClosestPoint(transform.position)));
        }
        Destroy(gameObject);
    }
}
