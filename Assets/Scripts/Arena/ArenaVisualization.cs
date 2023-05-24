using UnityEngine;

public class ArenaVisualization : MonoBehaviour
{
    public struct ArenaBounds
    {
        public ArenaBounds(float _minX, float _maxX, float _minZ, float _maxZ)
        {
            MinX = _minX;
            MaxX = _maxX;
            MinZ = _minZ;
            MaxZ = _maxZ;
        }

        public float MinX;
        public float MaxX;
        public float MinZ;
        public float MaxZ;
    }

    public bool IsInitialized { get; private set; } = false;

    [Header("References")]
    [SerializeField] private MeshFilter meshFilterComponent = null;

    private Vector3 arenaPosition = Vector3.zero;
    private Vector2 arenaSize = Vector2.zero;
    private ArenaBounds arenaBounds = new ArenaBounds();

    private Vector3[] vertices = new Vector3[0];
    private int[] triangles = new int[0];

    public void CreateArenaMesh(Vector3 _arenaPosition, Vector2 _arenaSize)
    {
        if(meshFilterComponent == null && TryGetComponent(out meshFilterComponent) == false)
        {
            Debug.LogError("Can't intitialize arena! Some references are null!", this);
            return;
        }

        arenaPosition = _arenaPosition;
        arenaSize = _arenaSize;
        arenaBounds = new ArenaBounds(-arenaSize.x /2f, arenaSize.x / 2f, -arenaSize.y / 2f, arenaSize.y / 2f);

        vertices = new Vector3[]
        {
            new Vector3(arenaBounds.MinX, 0f, arenaBounds.MinZ),
            new Vector3(arenaBounds.MaxX, 0f, arenaBounds.MinZ),
            new Vector3(arenaBounds.MinX, 0f, arenaBounds.MaxZ),
            new Vector3(arenaBounds.MaxX, 0f, arenaBounds.MaxZ)
        };

        triangles = new int[]
        {
            0, 2, 1,
            1, 2, 3
        };

        meshFilterComponent.mesh.vertices = vertices;
        meshFilterComponent.mesh.triangles = triangles;

        meshFilterComponent.mesh.RecalculateNormals();

        IsInitialized = true;
    }

    public Vector3 GetRandomPositionInsideArenaBounds()
    {
        float _randomX = Random.Range(arenaBounds.MinX, arenaBounds.MaxX);
        float _randomZ = Random.Range(arenaBounds.MinZ, arenaBounds.MaxZ);

        //TODO include actor radius into calculations
        return arenaPosition + new Vector3(_randomX, 0f, _randomZ);
    }
}
