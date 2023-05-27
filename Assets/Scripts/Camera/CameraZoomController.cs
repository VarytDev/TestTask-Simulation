using Cinemachine;
using System;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
class CameraZoomController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineFreeLook freeLookCameraComponent = null;

    [Header("Settings")]
    [AxisStateProperty] public AxisState zAxis = new AxisState(0, 1, false, true, 50f, 0.1f, 0.1f, "Mouse ScrollWheel", true);

    [Tooltip("Max zoom")]
    [Range(0.01f, 1f)]
    [SerializeField] private float minScale = 0.5f;

    [Tooltip("Min zoom")]
    [Range(1f, 5f)]
    [SerializeField] private float maxScale = 1;

    //Other
    private CinemachineFreeLook.Orbit[] originalOrbits = new CinemachineFreeLook.Orbit[0];

    private void OnValidate()
    {
        minScale = Mathf.Max(0.01f, minScale);
        maxScale = Mathf.Max(minScale, maxScale);
    }

    private void Awake()
    {
        if (freeLookCameraComponent == null && TryGetComponent(out freeLookCameraComponent) == false)
        {
            Debug.LogError("CameraZoomController :: Some references are null!", this);
            return;
        }

        originalOrbits = new CinemachineFreeLook.Orbit[freeLookCameraComponent.m_Orbits.Length];
        Array.Copy(freeLookCameraComponent.m_Orbits, originalOrbits, originalOrbits.Length);

        updateOrbits();
    }

    private void Update()
    {
        if (freeLookCameraComponent == null)
        {
            return;
        }

        updateOrbits();
    }

    private void updateOrbits()
    {
        zAxis.Update(Time.deltaTime);
        float scale = Mathf.Lerp(minScale, maxScale, zAxis.Value);

        for (int i = 0; i < Mathf.Min(originalOrbits.Length, freeLookCameraComponent.m_Orbits.Length); i++)
        {
            freeLookCameraComponent.m_Orbits[i].m_Height = originalOrbits[i].m_Height * scale;
            freeLookCameraComponent.m_Orbits[i].m_Radius = originalOrbits[i].m_Radius * scale;
        }
    }
}
