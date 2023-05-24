using System.Collections;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject agentPrefab = null;

    [Header("Agent Spawn Settings")]
    [SerializeField][Range(3,5)] private int startAgentsCount = 3;
    [SerializeField] private int maxAgentsCount = 30;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(2f, 6f);

    [Header("Spawned Agent Default Settings")]
    [SerializeField] private float spawnedAgentDefaultSpeed = 5f;
    [SerializeField] private int spawnedAgentDefaultHealth = 3;

    private int spawnedAgentsCount = 0;
    private ArenaVisualization arenaVisualizationComponent = null;
    private Coroutine agentSpawnCoroutine = null;

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
    }

    private void spawnInitialActors()
    {
        for (int i = 0; i < startAgentsCount; i++)
        {
            spawnAgent();
        }
    }

    //TODO Separate all spawn counter logic to another class?

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

    private void spawnAgent()
    {
        //TODO Add poolable agents

        if (agentPrefab == null || arenaVisualizationComponent == null || arenaVisualizationComponent.IsInitialized == false)
        {
            Debug.LogError("AgentSpawner :: Can't spawn agent! Some references are null!", this);
            return;
        }
        
        spawnedAgentsCount++;

        GameObject _newAgent = Instantiate(agentPrefab, arenaVisualizationComponent.GetRandomPositionInsideArenaBounds(), Quaternion.identity, transform);
        AgentHandler _agentHandler = _newAgent.GetComponent<AgentHandler>();

        if (_agentHandler == null)
        {
            Debug.LogError("AgentSpawner :: Can't find AgnetHandler attached to agent prefab! Aborting agent initialization...", this);
            return;
        }

        _agentHandler.InitializeAgent(arenaVisualizationComponent, spawnedAgentDefaultSpeed, spawnedAgentDefaultHealth);
        _agentHandler.AgentHealthComponent.OnAgentDeath += onAgentDeath;
    }

    private void onAgentDeath(AgentHealth _sender)
    {
        spawnedAgentsCount--;
        _sender.OnAgentDeath -= onAgentDeath;
    }
}
