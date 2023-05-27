using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraMovementController : MonoBehaviour
{
    private const string X_AXIS_NAME = "Mouse X";
    private const string Y_AXIS_NAME = "Mouse Y";

    [SerializeField] private CinemachineFreeLook freeLookCameraComponent = null;

    private void Start()
    {
        if (freeLookCameraComponent == null && TryGetComponent(out freeLookCameraComponent) == false)
        {
            Debug.LogError("CameraMovementController :: Some references are null!", this);
            return;
        }

        freeLookCameraComponent.m_XAxis.m_InputAxisName = "";
        freeLookCameraComponent.m_YAxis.m_InputAxisName = "";
    }

    private void Update()
    {
        if (freeLookCameraComponent == null)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            freeLookCameraComponent.m_XAxis.m_InputAxisValue = Input.GetAxis(X_AXIS_NAME);
            freeLookCameraComponent.m_YAxis.m_InputAxisValue = Input.GetAxis(Y_AXIS_NAME);
        }
        else
        {
            freeLookCameraComponent.m_XAxis.m_InputAxisValue = 0;
            freeLookCameraComponent.m_YAxis.m_InputAxisValue = 0;
        }
    }
}
