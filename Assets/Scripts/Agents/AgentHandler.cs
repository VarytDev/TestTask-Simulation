using DG.Tweening;
using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    public delegate void AgentDeathDelegate(AgentHandler _sender);
    public AgentDeathDelegate OnAgentDeath;

    private ArenaVisualization arenaVisualization = null;

    private float currentSpeed = 0f;
    private int currentHealth = 0;

    private Tween movementTween = null;

    public void InitializeAgent(ArenaVisualization _arenaVisualization, float _initialSpeed, int _initialHealth)
    {
        arenaVisualization = _arenaVisualization;

        currentSpeed = _initialSpeed;
        currentHealth = _initialHealth;

        startWandering();
    }

    private void startWandering()
    {
        moveToRandomPosition(startWandering);
    }

    private void moveToRandomPosition(TweenCallback _onCompleateCallback = null)
    {
        if (arenaVisualization == null || arenaVisualization.IsInitialized == false)
        {
            Debug.LogError("AgentHandler :: Can't access ArenaVisualization!", this);
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

    private void onAgentDeath()
    {
        OnAgentDeath?.Invoke(this);
    }
}
