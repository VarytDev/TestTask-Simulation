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
    private ArenaVisualization arenaVisualization = null;
    private Coroutine agentSpawnCoroutine = null;

    public void InitializeSpawner(ArenaVisualization _targetArena)
    {
        arenaVisualization = _targetArena;

        if (arenaVisualization == null || arenaVisualization.IsInitialized == false)
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
            spawnActor();
        }
    }

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

        spawnActor();
        startAgentSpawnCounter();
    }

    private void spawnActor()
    {
        if (agentPrefab == null || arenaVisualization == null || arenaVisualization.IsInitialized == false)
        {
            Debug.LogError("AgentSpawner :: Can't spawn agent! Some references are null!", this);
            return;
        }

        Instantiate(agentPrefab, arenaVisualization.GetRandomPositionInsideArenaBounds(), Quaternion.identity, transform);
    }
}
