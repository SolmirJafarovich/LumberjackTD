using UnityEngine;
using System.Collections.Generic;

public class LevelObjectSpawner
{
    private readonly GridFactory factory;
    private readonly GameObject wallPrefab;
    private readonly GameObject testTowerPrefab;
    private readonly List<GameObject> spawnedObjects = new();

    public LevelObjectSpawner(GridFactory factory, GameObject wallPrefab, GameObject testTowerPrefab)
    {
        this.factory = factory;
        this.wallPrefab = wallPrefab;
        this.testTowerPrefab = testTowerPrefab;
    }

    public void PlaceObjectAt(Vector2Int pos, GameObject prefab)
    {
        var node = factory.GetNode(pos);
        if (node == null || node.PlacedObject != null) return;

        Vector3 worldPos = new(pos.x, 1f, pos.y);
        GameObject instance = Object.Instantiate(prefab, worldPos, Quaternion.identity);
        node.PlacedObject = instance;
        spawnedObjects.Add(instance);
    }

    public void ClearObjects()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
                Object.Destroy(obj);
        }

        spawnedObjects.Clear();

        foreach (var node in factory.GetAllNodes())
        {
            node.PlacedObject = null;
        }
    }

    public void PlaceWall(Vector2Int pos)
    {
        PlaceObjectAt(pos, wallPrefab);
    }

    public void PlaceTower(Vector2Int pos)
    {
        PlaceObjectAt(pos, testTowerPrefab);
    }

}
