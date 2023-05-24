using DG.Tweening;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    private ArenaVisualization arenaVisualization = null;
    private float currentSpeed = 0f;

    private Tween movementTween = null;

    public void InitializeMovement(ArenaVisualization _arenaVisualization, float _initialSpeed)
    {
        if (_arenaVisualization == null || _arenaVisualization.IsInitialized == false)
        {
            Debug.LogError("AgentMovement :: Can't initialize agent! Some references are null...", this);
            return;
        }

        arenaVisualization = _arenaVisualization;
        currentSpeed = _initialSpeed;

        IsInitialized = true;
    }

    public void StartWandering()
    {
        moveToRandomPosition(StartWandering);
    }

    private void moveToRandomPosition(TweenCallback _onCompleateCallback = null)
    {
        if (IsInitialized == false)
        {
            return;
        }

        moveToPosition(arenaVisualization.GetRandomPositionInsideArenaBounds(), _onCompleateCallback);
    }

    private void moveToPosition(Vector3 _targetPosition, TweenCallback _onCompleateCallback = null)
    {
        if (movementTween.IsActive() == true)
        {
            movementTween.Kill();
        }

        movementTween = transform.DOMove(_targetPosition, getMovementTime(_targetPosition))
            .OnComplete(_onCompleateCallback);
    }

    private float getMovementTime(Vector3 _targetPosition)
    {
        return (_targetPosition - transform.position).magnitude / currentSpeed;
    }
}
