using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private readonly GridFactory factory;

    public Pathfinder(GridFactory factory)
    {
        this.factory = factory;
    }

    public List<GridNode> FindPath(Vector2Int start, Vector2Int end)
    {
        var open = new Queue<GridNode>();
        var cameFrom = new Dictionary<GridNode, GridNode>();
        var visited = new HashSet<GridNode>();

        GridNode startNode = factory.GetNode(start);
        GridNode endNode = factory.GetNode(end);

        open.Enqueue(startNode);
        visited.Add(startNode);

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (current == endNode)
            {
                List<GridNode> path = new();
                while (current != startNode)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Add(startNode);
                path.Reverse();
                return path;
            }

            foreach (var neighbor in GetNeighbours(current))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    open.Enqueue(neighbor);
                }
            }
        }

        return null;
    }

    private IEnumerable<GridNode> GetNeighbours(GridNode node)
    {
        Vector2Int[] dirs = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };

        foreach (var dir in dirs)
        {
            Vector2Int pos = node.GridPosition + dir;
            var neighbor = factory.GetNode(pos);
            if (neighbor != null && neighbor.Type != NodeType.Blocked)
                yield return neighbor;
        }
    }
}
