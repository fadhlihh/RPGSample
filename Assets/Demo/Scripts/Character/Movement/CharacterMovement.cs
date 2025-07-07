using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    protected float _walkSpeed = 1f;
    [SerializeField]
    protected float _sprintSpeed = 2f;
    [SerializeField]
    protected float _acceleration = .25f;

    protected float _velocityY;
    protected Vector3 _velocityXZ;
    protected Vector3 _velocity;
    protected float _currentSpeed;

    public float WalkSpeed { get { return _walkSpeed; } set { _walkSpeed = value; } }
    public float SprintSpeed { get { return _sprintSpeed; } set { _sprintSpeed = value; } }
    public float Acceleration { get { return _acceleration; } set { _acceleration = value; } }
    public bool IsGrounded { get; protected set; }
    public bool IsFalling { get { return !IsGrounded; } }
    public bool IsSprint { get; protected set; }
    public Vector3 Velocity { get { return _velocity; } }
    public Vector3 VelocityXZ { get { return new Vector3(_velocity.x, 0, _velocity.z); } }
    public float VelocityY { get { return _velocity.y; } }
    public bool IsAbleToMove { get; set; } = true;

    protected virtual void Update()
    {
        CheckIsGrounded();
    }

    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        IsGrounded = Physics.CheckSphere(transform.position, .5f, groundLayer);
    }
}
