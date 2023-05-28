using UnityEngine;

public class AgentHandler : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;
    public int AgentNumber { get; private set; } = 0;

    [Header("References")]
    public AgentHealth AgentHealthComponent = null;

    [SerializeField] private AgentMovement agentMovementComponent = null;
    [SerializeField] private AgentSelectionHandler agentSelectionHandlerComponent = null;
    [SerializeField] private AgentWeapon agentWeaponComponent = null;

    public void InitializeAgent(ArenaVisualization _arenaVisualization, float _initialSpeed, int _initialHealth, int _agentNumber, int _weaponDamage)
    {
        if (_arenaVisualization == null || _arenaVisualization.IsInitialized == false || agentMovementComponent == null || AgentHealthComponent == null || agentSelectionHandlerComponent == null)
        {
            Debug.LogError("AgentHandler :: Can't initialize agent! Some references are null...", this);
        }

        AgentNumber = _agentNumber;

        if (agentMovementComponent.TryInitializeMovement(_arenaVisualization, AgentHealthComponent, _initialSpeed) == false)
        {
            Debug.LogError("AgentHandler :: Failed to initialize movement!", this);
            return;
        }

        if (AgentHealthComponent.TryInitializeHealth(_initialHealth) == false)
        {
            Debug.LogError("AgentHandler :: Failed to initialize health!", this);
            return;
        }

        agentWeaponComponent.InitializeWeapon(_weaponDamage);

        agentMovementComponent.StartWandering();

        subscribeToEvents();
        IsInitialized = true;
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
