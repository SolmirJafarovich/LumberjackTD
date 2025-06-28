using UnityEngine;

public class TowerBuilder
{
    private readonly GridFactory gridFactory;
    private readonly LevelObjectSpawner objectSpawner;
    private readonly float buildTime;

    private float buildProgress;
    private GridNode currentTarget;

    public TowerBuilder(GridFactory gridFactory, LevelObjectSpawner objectSpawner, float buildTime = 5f)
    {
        this.gridFactory = gridFactory;
        this.objectSpawner = objectSpawner;
        this.buildTime = buildTime;
    }


    public float UpdateBuild(RaycastHit hit, bool isHoldingInteractKey)
    {
        if (!isHoldingInteractKey)
        {
            Reset();
            return 0f;
        }

        Vector3 worldPos = hit.point;
        Vector2Int gridPos = new(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
        var node = gridFactory.GetNode(gridPos);

        if (node == null || node.Type != NodeType.Free || node.PlacedObject != null)
        {
            Reset();
            return 0f;
        }

        if (node != currentTarget)
        {
            currentTarget = node;
            buildProgress = 0f;
        }

        buildProgress += Time.deltaTime;

        if (buildProgress >= buildTime)
        {
            node.Type = NodeType.Blocked;
            objectSpawner.PlaceTower(gridPos);
            Reset();
            return 0f;
        }

        return buildProgress / buildTime;
    }

    private void Reset()
    {
        currentTarget = null;
        buildProgress = 0f;
    }
}
