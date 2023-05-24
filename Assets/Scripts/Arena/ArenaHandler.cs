using UnityEngine;

public class ArenaHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ArenaVisualization arenaVisualization = null;
    [SerializeField] private AgentSpawner agentSpawner = null;

    [Header("Arena Settings")]
    [SerializeField] private Vector3 initialArenaPosition = Vector3.zero;
    [SerializeField] private Vector2 initialArenaSize = new Vector2(10f, 10f);

    private void Start()
    {
        InitializeArena();
    }

    public void InitializeArena()
    {
        if (arenaVisualization == null || agentSpawner == null)
        {
            Debug.LogError("ArenaHandler :: Can't initialize! Some references are null!", this);
            return;
        }

        arenaVisualization.CreateArenaMesh(initialArenaPosition, initialArenaSize);
        agentSpawner.InitializeSpawner(arenaVisualization);
    }
}
