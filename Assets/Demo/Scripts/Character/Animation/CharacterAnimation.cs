using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;

    protected Character _ownerCharacter;

    public void PlayFootstepSFX()
    {
        SFXManager.Instance.PlayAudioWithRandomPitch(ESFXType.Footstep, 0.5f, 1);
    }

    protected void Awake()
    {
        if (!_animator)
        {
            _animator = GetComponentInChildren<Animator>();
            if (!_animator)
            {
                _animator = GetComponent<Animator>();
            }
        }
    }
    protected void Start()
    {
        _ownerCharacter = GetComponent<Character>();
    }

    protected virtual void Update()
    {
        if (_ownerCharacter.CharacterMovement != null)
        {
            _animator.SetFloat("Velocity", _ownerCharacter.CharacterMovement.VelocityXZ.magnitude);
            _animator.SetFloat("VelocityX", _ownerCharacter.CharacterMovement.VelocityXZ.x);
            _animator.SetFloat("VelocityZ", _ownerCharacter.CharacterMovement.VelocityXZ.z);
            _animator.SetFloat("VelocityY", _ownerCharacter.CharacterMovement.VelocityY);
            _animator.SetBool("IsFalling", _ownerCharacter.CharacterMovement.IsFalling);
        }
    }
}
