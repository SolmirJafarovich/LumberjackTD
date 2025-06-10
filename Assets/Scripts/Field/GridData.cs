using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Dictionary<Vector2Int, GridNode> grid = new();

    public int Width { get; private set; }
    public int Height { get; private set; }

    public GridData(int width, int height)
    {
        Width = width;
        Height = height;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid.Clear();
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector2Int pos = new(x, y);
                grid[pos] = new GridNode(pos);
            }
        }
    }

    public void SetNodeType(Vector2Int pos, NodeType type)
    {
        if (grid.ContainsKey(pos))
        {
            grid[pos].Type = type;
        }
    }

    public GridNode GetNode(Vector2Int pos) => grid.ContainsKey(pos) ? grid[pos] : null;

    public IEnumerable<GridNode> GetAllNodes() => grid.Values;

    public List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new();
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

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

    public void ResetGrid() => GenerateGrid();
}
