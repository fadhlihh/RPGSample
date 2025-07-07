using UnityEngine;

public class DirectionalCharacterMovement : ControllerCharacterMovement
{
    [Header("Movement Rotation")]
    [SerializeField]
    protected bool _useCameraRotation;
    [SerializeField]
    private bool _orientRotationToMovement;
    [SerializeField]
    private float _orientRotationTime = .1f;

    private float _orientrotationVelocity;

    public bool UseCameraRotation { get { return _useCameraRotation; } set { _useCameraRotation = value; } }
    public bool OrientRotationToMovement { get { return _orientRotationToMovement; } set { _orientRotationToMovement = value; } }
    public float OrientRotationTime { get { return _orientRotationTime; } set { _orientRotationTime = value; } }

    protected override void CalculateVelocityXZ()
    {
        RotateCharacter();
        if (_orientRotationToMovement)
        {
            _velocityXZ = transform.rotation * Vector3.forward;
        }
        else
        {
            Transform camera = Camera.main.transform;
            Vector3 xDirection = MoveDirection.x * camera.right;
            Vector3 zDirection = MoveDirection.z * camera.forward;
            Vector3 direction = xDirection + zDirection;
            direction.y = 0;
            _velocityXZ = direction.normalized;
        }
    }

    protected override void RotateCharacter()
    {
        Transform cameraPosition = Camera.main.transform;
        if (_orientRotationToMovement)
        {
            if (MoveDirection.magnitude >= .01f)
            {
                float orientRotationAngle = GameHelper.GetRotationAngleFromInput(MoveDirection.x, MoveDirection.z) + (_useCameraRotation ? cameraPosition.eulerAngles.y : 0);
                float smoothRotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, orientRotationAngle, ref _orientrotationVelocity, _orientRotationTime);
                transform.rotation = Quaternion.Euler(0f, smoothRotationAngle, 0f);
            }
        }
    }

    public void AutoRotateCharacter()
    {
        Transform cameraPosition = Camera.main.transform;
        transform.rotation = cameraPosition.rotation;
    }

    protected override void Update()
    {
        base.Update();
        if (!_orientRotationToMovement)
        {
            AutoRotateCharacter();
        }
    }
}
