using UnityEngine;

public enum NodeType { Free, Blocked, Start, End }

public class GridNode
{
    public Vector2Int GridPosition { get; private set; }
    public bool IsWalkable => Type != NodeType.Blocked;
    public NodeType Type;

    // Äëÿ A*
    public float GCost;
    public float HCost;
    public float FCost => GCost + HCost;
    public GridNode Parent;

    public GridNode(Vector2Int pos, NodeType type = NodeType.Free)
    {
        GridPosition = pos;
        Type = type;
    }

    public void ResetPathfindingData()
    {
        GCost = 0;
        HCost = 0;
        Parent = null;
    }
}
