using DG.Tweening;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    private ArenaVisualization arenaVisualization = null;
    private AgentHealth agentHealthComponent = null;
    private float currentSpeed = 0f;

    private Tween movementTween = null;

    public bool TryInitializeMovement(ArenaVisualization _arenaVisualization, AgentHealth _agentHealth, float _initialSpeed)
    {
        if (_arenaVisualization == null || _arenaVisualization.IsInitialized == false || _agentHealth == null)
        {
            Debug.LogError("AgentMovement :: Can't initialize agent! Some references are null...", this);
            return false;
        }

        arenaVisualization = _arenaVisualization;
        agentHealthComponent = _agentHealth;
        currentSpeed = _initialSpeed;

        agentHealthComponent.OnAgentDeath += onAgentDeath;

        IsInitialized = true;
        return true;
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
            .OnComplete(_onCompleateCallback)
            .SetEase(Ease.Linear);
    }

    private void onAgentDeath(AgentHealth _sender)
    {
        if (movementTween.IsActive() == true)
        {
            movementTween.Kill();
        }

        agentHealthComponent.OnAgentDeath -= onAgentDeath;
    }

    private float getMovementTime(Vector3 _targetPosition)
    {
        return (_targetPosition - transform.position).magnitude / currentSpeed;
    }
}
