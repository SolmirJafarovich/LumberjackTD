using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public GameObject nodePrefab;
    public GameObject wallPrefab;
    [Range(0f, 1f)] public float blockedCellChance = 0.2f;

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public GameObject stepMarkerPrefab;

    private Dictionary<Vector2Int, GridNode> grid = new();
    private Dictionary<Vector2Int, GameObject> visuals = new();
    private Dictionary<Vector2Int, GameObject> wallVisuals = new();

    private Vector2Int startPos;
    private Vector2Int endPos;

    private Pathfinding pathfinding;

    void Start()
    {
        GenerateGrid();
        pathfinding = new Pathfinding(this);
        GeneratePlayableLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Regenerating level...");
            RegenerateLevel();
        }
    }

    public void RegenerateLevel()
    {
        GenerateGrid();
        GeneratePlayableLevel();
    }

    public void GenerateGrid()
    {
        ClearGrid();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new(x, y);
                GridNode node = new(pos);
                grid[pos] = node;

                GameObject visual = Instantiate(nodePrefab, new Vector3(x, 0, y), Quaternion.identity, transform);
                visual.name = $"Node_{x}_{y}";
                visuals[pos] = visual;
            }
        }
    }

    public void GeneratePlayableLevel()
    {
        int maxTries = 50;
        int tries = 0;

        do
        {
            GenerateRandomLevel();
            var path = pathfinding.FindPath(startPos, endPos);
            if (path != null && path.Count > 1)
            {
                Debug.Log($"Path found after {tries + 1} attempt(s).");

                SpawnEnemyAndRunPath(path.ConvertAll(node => node.GridPosition));
                break;
            }
            tries++;
        } while (tries < maxTries);

        if (tries >= maxTries)
        {
            Debug.LogWarning("Could not generate a playable level after max attempts.");
        }
    }

    public void GenerateRandomLevel()
    {
        // Находим старт и финиш, которые находятся не ближе 5 клеток
        do
        {
            startPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
            endPos = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (ManhattanDistance(startPos, endPos) < 5);

        foreach (var kvp in grid)
        {
            Vector2Int pos = kvp.Key;

            if (pos == startPos)
            {
                SetNodeType(pos, NodeType.Start);
            }
            else if (pos == endPos)
            {
                SetNodeType(pos, NodeType.End);
            }
            else
            {
                bool blocked = Random.value < blockedCellChance;
                SetNodeType(pos, blocked ? NodeType.Blocked : NodeType.Free);
            }
        }
    }

    private int ManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }


    public void SpawnEnemyAndRunPath(List<Vector2Int> path)
    {
        if (enemyPrefab == null || stepMarkerPrefab == null)
        {
            Debug.LogWarning("Enemy or Step Marker prefab is not assigned.");
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, GridToWorld(path[0]), Quaternion.identity);
        var mover = enemy.AddComponent<EnemyMover>();
        mover.stepMarkerPrefab = stepMarkerPrefab;
        mover.Init(this, path);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, 0.5f, gridPos.y);
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        grid.Clear();
        visuals.Clear();
        wallVisuals.Clear();
    }

    public GridNode GetNode(Vector2Int pos)
    {
        return grid.ContainsKey(pos) ? grid[pos] : null;
    }

    public IEnumerable<GridNode> GetAllNodes() => grid.Values;

    public List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new();
        Vector2Int[] dirs = {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var dir in dirs)
        {
            Vector2Int checkPos = node.GridPosition + dir;
            if (grid.ContainsKey(checkPos))
            {
                neighbours.Add(grid[checkPos]);
            }
        }

        return neighbours;
    }

    public void SetNodeType(Vector2Int pos, NodeType type)
    {
        if (!grid.ContainsKey(pos)) return;

        grid[pos].Type = type;

        var renderer = visuals[pos].GetComponent<Renderer>();
        if (renderer != null)
        {
            switch (type)
            {
                case NodeType.Free:
                    renderer.material.color = Color.white;
                    DestroyWallAt(pos);
                    break;
                case NodeType.Blocked:
                    renderer.material.color = Color.gray;
                    CreateWallAt(pos);
                    break;
                case NodeType.Start:
                    renderer.material.color = Color.green;
                    DestroyWallAt(pos);
                    break;
                case NodeType.End:
                    renderer.material.color = Color.red;
                    DestroyWallAt(pos);
                    break;
            }
        }
    }

    private void CreateWallAt(Vector2Int pos)
    {
        if (wallPrefab == null || wallVisuals.ContainsKey(pos)) return;

        Vector3 wallPos = new Vector3(pos.x, 1f, pos.y); // выше на 1 единицу
        GameObject wall = Instantiate(wallPrefab, wallPos, Quaternion.identity, transform);
        wall.name = $"Wall_{pos.x}_{pos.y}";
        wallVisuals[pos] = wall;
    }

    private void DestroyWallAt(Vector2Int pos)
    {
        if (wallVisuals.TryGetValue(pos, out var wall))
        {
            Destroy(wall);
            wallVisuals.Remove(pos);
        }
    }

    public Vector2Int GetStartPosition() => startPos;
    public Vector2Int GetEndPosition() => endPos;
}
