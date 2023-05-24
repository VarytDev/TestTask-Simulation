using UnityEngine;

public class AgentHealth : MonoBehaviour
{
    public delegate void AgentDeathDelegate(AgentHealth _sender);
    public AgentDeathDelegate OnAgentDeath;

    private int currnetHealth = 0;

    public void InitializeHealth(int _initialHealth)
    {
        currnetHealth = _initialHealth;
    }

    private void onAgentDeath()
    {
        OnAgentDeath?.Invoke(this);
    }
}
