using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    private CharacterMovement _characterMovement;
    [SerializeField]
    private CharacterAnimation _characterAnimation;
    [SerializeField]
    private Transform _lookPivot;

    public UnityEvent OnDamage;
    public UnityEvent OnDeath;

    public CharacterMovement CharacterMovement { get { return _characterMovement; } }
    public CharacterAnimation CharacterAnimation { get { return _characterAnimation; } }
    public Transform LookPivot { get => _lookPivot; }

    protected virtual void Awake()
    {
        if (!_characterMovement)
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        if (!_characterAnimation)
        {
            _characterAnimation = GetComponent<CharacterAnimation>();
        }

        if (!_lookPivot)
        {
            _lookPivot = transform.Find("LookPivot");
        }
    }
}
