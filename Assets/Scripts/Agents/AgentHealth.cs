using UnityEngine;

public class AgentHealth : MonoBehaviour, IDamagable
{
    public delegate void AgentDamageTakenDelegate(AgentHealth _sender);
    public AgentDamageTakenDelegate OnAgentDamageTaken;

    public delegate void AgentDeathDelegate(AgentHealth _sender);
    public AgentDeathDelegate OnAgentDeath;

    public int CurrnetHealth { get; private set; } = 0;

    [SerializeField] private GameObject rootObject = null;

    public void InitializeHealth(int _initialHealth)
    {
        CurrnetHealth = _initialHealth;
    }

    public void OnDamageTaken(int _damageToDeal)
    {
        CurrnetHealth -= _damageToDeal;

        OnAgentDamageTaken?.Invoke(this);

        if (CurrnetHealth <= 0)
        {
            onAgentDeath();
        }
    }

    private void onAgentDeath()
    {
        OnAgentDeath?.Invoke(this);

        if(rootObject == null)
        {
            return;
        }

        Destroy(rootObject);
    }
}
