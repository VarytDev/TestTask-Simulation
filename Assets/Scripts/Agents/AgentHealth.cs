using UnityEngine;

public class AgentHealth : MonoBehaviour, IDamagable
{
    public delegate void AgentDamageTakenDelegate(AgentHealth _sender);
    public AgentDamageTakenDelegate OnAgentDamageTaken;

    public delegate void AgentDeathDelegate(AgentHealth _sender);
    public AgentDeathDelegate OnAgentDeath;

    public bool IsInitialized { get; private set; } = false;
    public int CurrnetHealth { get; private set; } = 0;

    [Header("References")]
    [SerializeField] private GameObject rootObject = null;

    public bool TryInitializeHealth(int _initialHealth)
    {
        CurrnetHealth = _initialHealth;

        if (checkIfDead() == true)
        {
            return false;
        }

        IsInitialized = true;
        return true;
    }

    public void OnDamageTaken(int _damageToDeal)
    {
        CurrnetHealth -= _damageToDeal;

        OnAgentDamageTaken?.Invoke(this);

        checkIfDead();
    }

    private bool checkIfDead()
    {
        if (CurrnetHealth > 0)
        {
            return false;
        }

        onAgentDeath();
        return true;
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
