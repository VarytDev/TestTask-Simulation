using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    public bool IsInitialized { get; private set; } = false;

    [Header("References")]
    [SerializeField] private ArenaVisualization arenaVisualizationComponent = null;
    [SerializeField] private AgentSpawner agentSpawnerComponent = null;

    [Header("Arena Settings")]
    [SerializeField] private Vector3 initialArenaPosition = Vector3.zero;
    [SerializeField] private Vector2 initialArenaSize = new Vector2(10f, 10f);
    [SerializeField] private float agentRadius = 0.5f;

    private void Start()
    {
        InitializeArena();
    }

    public void InitializeArena()
    {
        if (arenaVisualizationComponent == null || agentSpawnerComponent == null)
        {
            Debug.LogError("ArenaHandler :: Can't initialize! Some references are null!", this);
            return;
        }

        arenaVisualizationComponent.CreateArenaMesh(initialArenaPosition, initialArenaSize, agentRadius);
        agentSpawnerComponent.InitializeSpawner(arenaVisualizationComponent);

        IsInitialized = true;
    }
}
