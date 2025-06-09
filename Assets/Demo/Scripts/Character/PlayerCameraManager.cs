using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera _thirdPersonCamera;
    [SerializeField]
    private CinemachineCamera _lockCamera;
    [SerializeField]
    private CinemachineCamera _aimCamera;

    private CinemachineOrbitalFollow _orbitalFollow;
    private CinemachinePanTilt _aimPanTilt;

    private void Start()
    {
        _orbitalFollow = _thirdPersonCamera.GetComponent<CinemachineOrbitalFollow>();
        _aimPanTilt = _aimCamera.GetComponent<CinemachinePanTilt>();
        SwitchToThirdPersonCamera();
    }

    public void SwitchToThirdPersonCamera()
    {
        _orbitalFollow.HorizontalAxis.Value = transform.eulerAngles.y;
        _thirdPersonCamera.Priority.Value = 1;
        _lockCamera.Priority.Value = 0;
        _aimCamera.Priority.Value = 0;
    }

    public void SwitchToLockCamera()
    {
        _thirdPersonCamera.Priority.Value = 0;
        _lockCamera.Priority.Value = 1;
        _aimCamera.Priority.Value = 0;
    }

    public void SwitchToAimCamera()
    {
        _aimPanTilt.PanAxis.Value = transform.eulerAngles.y;
        _aimPanTilt.TiltAxis.Value = 0;
        _thirdPersonCamera.Priority.Value = 0;
        _lockCamera.Priority.Value = 0;
        _aimCamera.Priority.Value = 1;
    }
}
