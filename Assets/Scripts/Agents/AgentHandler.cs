using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    public int AgentNumber { get; private set; } = 0;

    [Header("References")]
    public AgentHealth AgentHealthComponent = null;
    [SerializeField] private AgentMovement agentMovementComponent = null;
    [SerializeField] private AgentSelectionHandler agentSelectionHandlerComponent = null;

    public void InitializeAgent(ArenaVisualization _arenaVisualization, float _initialSpeed, int _initialHealth, int _agentNumber)
    {
        if (_arenaVisualization == null || _arenaVisualization.IsInitialized == false || agentMovementComponent == null || AgentHealthComponent == null || agentSelectionHandlerComponent == null)
        {
            Debug.LogWarning("AgentHandler :: Can't initialize agent! Some references are null...", this);
        }

        AgentNumber = _agentNumber;

        agentMovementComponent.InitializeMovement(_arenaVisualization, _initialSpeed);
        AgentHealthComponent.InitializeHealth(_initialHealth);

        agentMovementComponent.StartWandering();

        subscribeToEvents();
    }

    private void subscribeToEvents()
    {
        agentSelectionHandlerComponent.OnAgentSelectionChanged += onAgentSelectionChanged;
        AgentHealthComponent.OnAgentDeath += onAgentDeath;
    }

    private void unsubscribeFromEvents()
    {
        agentSelectionHandlerComponent.OnAgentSelectionChanged -= onAgentSelectionChanged;
        AgentHealthComponent.OnAgentDeath -= onAgentDeath;
    }

    #region Callbacks
    private void onAgentDeath(AgentHealth _sender)
    {
        unsubscribeFromEvents();
    }

    private void onAgentSelectionChanged(bool _isSelected)
    {
        if (_isSelected == true)
        {
            UIManager.Instance.AddAgentToDisplay(this);
            return;
        }

        UIManager.Instance.RemoveAgentFromDisplay();
    } 
    #endregion
}
