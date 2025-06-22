using UnityEngine;

public class GridNode
{
    public Vector2Int GridPosition { get; private set; }
    public NodeType Type { get; set; }

    public GameObject PlacedObject { get; set; }

    public GridNode(Vector2Int pos)
    {
        GridPosition = pos;
        Type = NodeType.Free;
    }
}
