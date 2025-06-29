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
    private readonly LevelObjectSpawner objectSpawner;
    private readonly PlayerSpawner playerSpawner;
    private readonly DeathScreenUI deathScreenUI;
    private readonly PlayerHealthUIBinder healthUIBinder;

    public LevelSystem(GridSettings settings, GameObject nodePrefab, GameObject enemyPrefab, GameObject stepMarkerPrefab, GameObject testTowerPrefab, GameObject wallPrefab, GameObject playerPrefab, UIRingController ringUI, PlayerHealthUIBinder healthUIBinder, Transform parent)
    {
        gridFactory = new GridFactory(settings);
        gridFactory.GenerateGrid();

        pathfinder = new Pathfinder(gridFactory);
        gridVisual = new GridVisual(gridFactory, nodePrefab, parent);
        objectSpawner = new LevelObjectSpawner(gridFactory, wallPrefab, testTowerPrefab);
        levelGenerator = new LevelGenerator(gridFactory, gridVisual, settings, pathfinder, objectSpawner);
        enemySpawner = new EnemySpawner(enemyPrefab, stepMarkerPrefab, gridFactory);
        playerSpawner = new PlayerSpawner(gridFactory, playerPrefab, objectSpawner, ringUI, healthUIBinder);

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

            playerSpawner.SpawnPlayer(levelGenerator.EndPos);
        }
    }
}
