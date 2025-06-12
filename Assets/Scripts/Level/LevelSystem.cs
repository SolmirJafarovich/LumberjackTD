using UnityEngine;
using System.Collections.Generic;

public class LevelSystem
{
    private readonly GridFactory gridFactory;
    private readonly GridVisual gridVisual;
    private readonly LevelGenerator levelGenerator;
    private readonly EnemySpawner enemySpawner;
    private readonly Pathfinder pathfinder;
    private readonly LevelInputHandler inputHandler;

    public LevelSystem(GridSettings settings, GameObject nodePrefab, GameObject enemyPrefab, GameObject stepMarkerPrefab, Transform parent)
    {
        gridFactory = new GridFactory(settings);
        gridFactory.GenerateGrid();
        pathfinder = new Pathfinder(gridFactory);
        gridVisual = new GridVisual(gridFactory, nodePrefab, parent);
        levelGenerator = new LevelGenerator(gridFactory, gridVisual, settings, pathfinder);
        enemySpawner = new EnemySpawner(enemyPrefab, stepMarkerPrefab, gridFactory);
        inputHandler = new LevelInputHandler(GenerateLevel);


        GenerateLevel();
    }

    public void Update()
    {
        inputHandler.Update();
    }

    private void GenerateLevel()
    {
        if (levelGenerator.GeneratePlayableLevel())
        {
            var path = pathfinder.FindPath(levelGenerator.StartPos, levelGenerator.EndPos);
            enemySpawner.SpawnAndRun(path.ConvertAll(n => n.GridPosition));
        }
    }
}
