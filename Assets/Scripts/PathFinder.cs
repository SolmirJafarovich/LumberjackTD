using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private readonly GridManager grid;

    public Pathfinding(GridManager gridManager)
    {
        grid = gridManager;
    }

    public List<GridNode> FindPath(Vector2Int startPos, Vector2Int endPos)
    {
        GridNode startNode = grid.GetNode(startPos);
        GridNode endNode = grid.GetNode(endPos);

        foreach (var node in grid.GetAllNodes())
        {
            node.ResetPathfindingData();
        }

        List<GridNode> openSet = new() { startNode };
        HashSet<GridNode> closedSet = new();

        while (openSet.Count > 0)
        {
            GridNode current = GetLowestFCostNode(openSet);

            if (current == endNode)
                return ReconstructPath(endNode);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                    continue;

                float tentativeGCost = current.GCost + Vector2Int.Distance(current.GridPosition, neighbour.GridPosition);

                if (tentativeGCost < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = tentativeGCost;
                    neighbour.HCost = Vector2Int.Distance(neighbour.GridPosition, endNode.GridPosition);
                    neighbour.Parent = current;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null; // путь не найден
    }

    private GridNode GetLowestFCostNode(List<GridNode> nodes)
    {
        GridNode lowest = nodes[0];
        foreach (var node in nodes)
        {
            if (node.FCost < lowest.FCost)
                lowest = node;
        }
        return lowest;
    }

    private List<GridNode> ReconstructPath(GridNode endNode)
    {
        List<GridNode> path = new();
        GridNode current = endNode;
        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }
}
