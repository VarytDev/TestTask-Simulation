using UnityEngine;

public class AgentHealth : MonoBehaviour, IDamagable
{
    public delegate void AgentDeathDelegate(AgentHealth _sender);
    public AgentDeathDelegate OnAgentDeath;

    private int currnetHealth = 0;

    public void InitializeHealth(int _initialHealth)
    {
        currnetHealth = _initialHealth;
    }

    public void OnDamageTaken(int _damageToDeal)
    {
        currnetHealth -= _damageToDeal;

        if (currnetHealth <= 0)
        {
            onAgentDeath();
        }
    }

    private void onAgentDeath()
    {
        OnAgentDeath?.Invoke(this);

        Destroy(gameObject);
    }
}
