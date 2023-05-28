using DG.Tweening;
using UnityEngine;

public class WindowScroller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform openWindowTransform = null;
    [SerializeField] private Transform closeWindowTransform = null;

    [Header("Settings")]
    [SerializeField] private float windowMoveTime = 3f;

    private bool isWindowOpen = false;
    private Tween moveTween = null;

    public void ToggleWindowState()
    {
        SetWindowState(!isWindowOpen);
    }

    public void SetWindowState(bool _newWindowState)
    {
        if(openWindowTransform == null || closeWindowTransform == null)
        {
            Debug.LogError("WindowScroller :: Some references are null!", this);
        }

        Transform _targetTransform = isWindowOpen == true ? closeWindowTransform : openWindowTransform;

        float _moveTimeOverride = windowMoveTime;

        if(moveTween.IsActive() == true)
        {
            _moveTimeOverride = moveTween.Elapsed();
            moveTween.Kill();
        }

        moveTween = transform.DOMove(_targetTransform.position, _moveTimeOverride)
            .SetEase(Ease.InOutSine);

        isWindowOpen = _newWindowState;
    }
}
