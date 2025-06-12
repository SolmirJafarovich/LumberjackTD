using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSettings settings;
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject stepMarkerPrefab;

    private GridFactory gridFactory;
    private GridVisual gridVisual;
    private LevelGenerator levelGenerator;
    private EnemySpawner enemySpawner;
    private Pathfinder pathfinder;
    private LevelInputHandler inputHandler;

    void Start()
    {
        gridFactory = new GridFactory(settings);
        gridFactory.GenerateGrid();

        gridVisual = new GridVisual(gridFactory, nodePrefab, transform);
        pathfinder = new Pathfinder(gridFactory);
        levelGenerator = new LevelGenerator(gridFactory, gridVisual, settings, pathfinder);
        enemySpawner = new EnemySpawner(enemyPrefab, stepMarkerPrefab, gridFactory);
        inputHandler = new LevelInputHandler(GenerateLevel);


        GenerateLevel();
    }

    void Update()
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
