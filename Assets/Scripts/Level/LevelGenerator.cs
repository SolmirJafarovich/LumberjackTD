using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    private readonly GridFactory gridFactory;
    private readonly GridVisual gridVisual;
    private readonly GridSettings settings;
    private readonly Pathfinder pathfinder;
    private LevelObjectSpawner objectSpawner;
    public Vector2Int StartPos { get; private set; }
    public Vector2Int EndPos { get; private set; }

    public LevelGenerator(GridFactory gridFactory, GridVisual gridVisual, GridSettings settings, Pathfinder pathfinding, LevelObjectSpawner objectSpawner)
    {
        this.gridFactory = gridFactory;
        this.gridVisual = gridVisual;
        this.settings = settings;
        this.pathfinder = pathfinding;
        this.objectSpawner = objectSpawner;
    }

    public bool GeneratePlayableLevel()
    {
        int tries = 0;
        const int maxTries = 50;

        while (tries++ < maxTries)
        {

            GenerateRandomLevel();
            var path = pathfinder.FindPath(StartPos, EndPos);

            if (path != null && path.Count > 1)
            {
                Debug.Log($"Path found after {tries} attempt(s).");
                SpawnStaticObjects();
                return true;
            }
        }

        

        Debug.LogWarning("Could not generate a playable level after max attempts.");
        return false;
    }

    private void GenerateRandomLevel()
    {
        int maxDist = Mathf.Min(settings.Width, settings.Height) - 1;

        do
        {
            StartPos = new Vector2Int(Random.Range(0, settings.Width), Random.Range(0, settings.Height));
            EndPos = new Vector2Int(Random.Range(0, settings.Width), Random.Range(0, settings.Height));
        }
        while (Vector2Int.Distance(StartPos, EndPos) < 5);

        foreach (var node in gridFactory.GetAllNodes())
        {
            var pos = node.GridPosition;

            if (pos == StartPos)
            {
                node.Type = NodeType.Start;
            }
            else if (pos == EndPos)
            {
                node.Type = NodeType.End;
            }
            else
            {
                bool blocked = Random.value < settings.BlockedChance;
                node.Type = blocked ? NodeType.Blocked : NodeType.Free;


            }

            gridVisual.UpdateNodeVisual(node);
        }
    }

    private void SpawnStaticObjects()
    {
        objectSpawner.ClearObjects();

        foreach (var node in gridFactory.GetAllNodes())
        {
            if (node.Type == NodeType.Blocked)
            {
                objectSpawner.PlaceWall(node.GridPosition);
            }
        }

        objectSpawner.PlaceTower(new Vector2Int(2,2));



    }

}
