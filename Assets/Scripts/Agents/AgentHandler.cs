using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    [Header("References")]
    public AgentHealth AgentHealthComponent = null;
    [SerializeField] private AgentMovement agentMovementComponent = null;

    public void InitializeAgent(ArenaVisualization _arenaVisualization, float _initialSpeed, int _initialHealth)
    {
        if (_arenaVisualization == null || _arenaVisualization.IsInitialized == false)
        {
            Debug.LogWarning("AgentHandler :: Can't initialize agent! Some references are null...", this);
        }

        agentMovementComponent.InitializeMovement(_arenaVisualization, _initialSpeed);
        AgentHealthComponent.InitializeHealth(_initialHealth);

        agentMovementComponent.StartWandering();
    }
}
