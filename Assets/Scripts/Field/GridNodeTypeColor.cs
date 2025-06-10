using UnityEngine;

public static class GridNodeTypeColor
{
    public static Color GetColor(NodeType type)
    {
        return type switch
        {
            NodeType.Free => Color.white,
            NodeType.Blocked => Color.black,
            NodeType.Start => Color.green,
            NodeType.End => Color.red,
            _ => Color.gray,
        };
    }
}
