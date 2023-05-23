using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ArenaVisualization arenaVisualization = null;
    [SerializeField] private GameObject actorPrefab = null;

    [Header("Agent Spawn Settings")]
    [SerializeField][Range(3,5)] private int startAgentsCount = 3;
    [SerializeField] private int maxAgentsCount = 30;
    [SerializeField] private Vector2 spawnTimeRange = new Vector2(2f, 6f);

    [Header("Spawned Agent Default Settings")]
    [SerializeField] private float spawnedAgentDefaultSpeed = 5f;
    [SerializeField] private int spawnedAgentDefaultHealth = 3;

    public void InitializeSpawner()
    {
        for (int i = 0; i < startAgentsCount; i++)
        {
            spawnActor();
        }
    }

    private void spawnActor()
    {
        if (actorPrefab == null || arenaVisualization == null)
        {
            return;
        }

        Instantiate(actorPrefab, arenaVisualization.GetRandomPositionInsideArenaBounds(), Quaternion.identity, transform);
    }
}
