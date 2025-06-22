using System.Collections.Generic;
using UnityEngine;

public class GridFactory
{
    private readonly GridSettings settings;
    private readonly Dictionary<Vector2Int, GridNode> grid = new();

    public GridFactory(GridSettings settings)
    {
        this.settings = settings;
    }

    public void GenerateGrid()
    {
        grid.Clear();


        for (int x = 0; x < settings.Width; x++)
        {
            for (int y = 0; y < settings.Height; y++)
            {
                Vector2Int pos = new(x, y);
                grid[pos] = new GridNode(pos);
            }
        }
    }

    public GridNode GetNode(Vector2Int pos) => grid.ContainsKey(pos) ? grid[pos] : null;

    public IEnumerable<GridNode> GetAllNodes() => grid.Values;
}
