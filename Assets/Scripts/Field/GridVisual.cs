using System.Collections.Generic;
using UnityEngine;

public class GridVisual
{
    private readonly GridFactory factory;
    private readonly GameObject prefab;
    private readonly Transform parent;
    private readonly Dictionary<Vector2Int, GameObject> visuals = new();

    public GridVisual(GridFactory factory, GameObject prefab, Transform parent)
    {
        this.factory = factory;
        this.prefab = prefab;
        this.parent = parent;

        CreateVisuals();
    }

    private void CreateVisuals()
    {
        foreach (var node in factory.GetAllNodes())
        {
            Vector3 pos = new(node.GridPosition.x, 0, node.GridPosition.y);
            GameObject go = Object.Instantiate(prefab, pos, Quaternion.identity, parent);
            go.name = $"Node_{node.GridPosition.x}_{node.GridPosition.y}";
            visuals[node.GridPosition] = go;
        }
    }

    public void UpdateNodeVisual(GridNode node)
    {
        if (visuals.TryGetValue(node.GridPosition, out var go))
        {
            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = GridNodeTypeColor.GetColor(node.Type);
            }
        }
    }
}
