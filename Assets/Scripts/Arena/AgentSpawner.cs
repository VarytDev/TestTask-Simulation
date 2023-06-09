using System.Collections;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    private const int AGENTS_COUNT_LIMIT = 30;
    private const int AGENTS_COUNT_MIN = 1;

    public bool IsInitialized { get; private set; } = false;

    [Header("References")]
    [SerializeField] private GameObject agentPrefab = null;

    [Header("Agent Spawn Settings")]
    [SerializeField][Range(3,5)] private int startAgentsCount = 3;
    [SerializeField] private int maxAgentsCount = 30;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(2f, 6f);

    [Header("Spawned Agent Default Settings")]
    [SerializeField] private float spawnedAgentDefaultSpeed = 5f;
    [SerializeField] private int spawnedAgentDefaultHealth = 3;
    [SerializeField] private int spawnedAgentDefaultDamage = 1;

    private int agentsTotalCount = 0;
    private int spawnedAgentsCount = 0;
    private ArenaVisualization arenaVisualizationComponent = null;
    private Coroutine agentSpawnCoroutine = null;

    private void OnValidate()
    {
        maxAgentsCount = Mathf.Max(maxAgentsCount, AGENTS_COUNT_MIN);
        maxAgentsCount = Mathf.Min(maxAgentsCount, AGENTS_COUNT_LIMIT);
    }

    public void InitializeSpawner(ArenaVisualization _targetArena)
    {
        arenaVisualizationComponent = _targetArena;

        if (arenaVisualizationComponent == null || arenaVisualizationComponent.IsInitialized == false)
        {
            Debug.LogError("AgentSpawner :: ArenaVisualisation isn't valid. Aborting initialization!", this);
            return;
        }

        spawnInitialActors();
        startAgentSpawnCounter();

        IsInitialized = true;
    }

    private void spawnInitialActors()
    {
        for (int i = 0; i < startAgentsCount; i++)
        {
            spawnAgent();
        }
    }

    private void spawnAgent()
    {
        if (agentPrefab == null || arenaVisualizationComponent == null || arenaVisualizationComponent.IsInitialized == false)
        {
            Debug.LogError("AgentSpawner :: Can't spawn agent! Some references are null!", this);
            return;
        }

        if (spawnedAgentsCount >= maxAgentsCount)
        {
            return;
        }

        agentsTotalCount++;
        spawnedAgentsCount++;

        GameObject _newAgent = Instantiate(agentPrefab, arenaVisualizationComponent.GetRandomPositionInsideArenaBounds(), Quaternion.identity, transform);
        AgentHandler _agentHandler = _newAgent.GetComponent<AgentHandler>();

        if (_agentHandler == null)
        {
            Debug.LogError("AgentSpawner :: Can't find AgnetHandler attached to agent prefab! Aborting agent initialization...", this);
            return;
        }

        _agentHandler.InitializeAgent(arenaVisualizationComponent, spawnedAgentDefaultSpeed, spawnedAgentDefaultHealth, agentsTotalCount, spawnedAgentDefaultDamage);
        _agentHandler.AgentHealthComponent.OnAgentDeath += onAgentDeath;
    }

    private void onAgentDeath(AgentHealth _sender)
    {
        spawnedAgentsCount--;
        _sender.OnAgentDeath -= onAgentDeath;
    }

    #region Spawner Counter
    private void startAgentSpawnCounter()
    {
        stopAgentSpawnCoroutine();

        agentSpawnCoroutine = StartCoroutine(agentSpawnCounter());
    }

    private void stopAgentSpawnCoroutine()
    {
        if (agentSpawnCoroutine != null)
        {
            StopCoroutine(agentSpawnCoroutine);
            agentSpawnCoroutine = null;
        }
    }

    private IEnumerator agentSpawnCounter()
    {
        while (spawnedAgentsCount >= maxAgentsCount)
        {
            yield return null;
        }

        yield return Waiters.WaitForSeconds(Random.Range(spawnTimeRange.x, spawnTimeRange.y));

        spawnAgent();
        startAgentSpawnCounter();
    }
    #endregion
}
