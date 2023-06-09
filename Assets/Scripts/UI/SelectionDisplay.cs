using TMPro;
using UnityEngine;

public class SelectionDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI agentNameField = null;
    [SerializeField] private TextMeshProUGUI agentHealthField = null;

    private AgentHandler currentHandler = null;

    public void EnableDisplay(AgentHandler _targetAgent)
    {
        if (_targetAgent == null)
        {
            return;
        }

        currentHandler = _targetAgent;

        currentHandler.AgentHealthComponent.OnAgentDamageTaken += onAgentTakenDamage;
        currentHandler.AgentHealthComponent.OnAgentDeath += onAgentDeath;

        setDisplayWindowState(true);
        updateDisplayData();
    }

    public void DisableDisplay()
    {
        setDisplayWindowState(false);

        if (currentHandler == null)
        {
            return;
        }

        currentHandler.AgentHealthComponent.OnAgentDamageTaken -= onAgentTakenDamage;
        currentHandler.AgentHealthComponent.OnAgentDeath -= onAgentDeath;
        currentHandler = null;
    }

    private void onAgentTakenDamage(AgentHealth _sender)
    {
        if (currentHandler == null)
        {
            return;
        }

        updateDisplayData();
    }

    private void onAgentDeath(AgentHealth _sender)
    {
        DisableDisplay();
    }

    private void updateDisplayData()
    {
        if (currentHandler == null || agentNameField == null || agentHealthField == null)
        {
            Debug.LogError("SelectionDisplay :: Some references are null. Can't update data!", this);
            return;
        }

        agentNameField.text = $"Agent #{currentHandler.AgentNumber}";
        agentHealthField.text = $"Health: {currentHandler.AgentHealthComponent.CurrnetHealth}";
    }

    private void setDisplayWindowState(bool _newState)
    {
        if (gameObject.activeSelf != _newState)
        {
            gameObject.SetActive(_newState);
        }
    }
}
