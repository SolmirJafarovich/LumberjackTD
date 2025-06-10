using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private readonly GameObject enemyPrefab;
    private readonly GameObject stepMarkerPrefab;
    private readonly GridFactory gridFactory;

    public EnemySpawner(GameObject enemyPrefab, GameObject stepMarkerPrefab, GridFactory gridFactory)
    {
        this.enemyPrefab = enemyPrefab;
        this.stepMarkerPrefab = stepMarkerPrefab;
        this.gridFactory = gridFactory;
    }

    public void SpawnAndRun(List<Vector2Int> path)
    {
        if (enemyPrefab == null || stepMarkerPrefab == null)
        {
            Debug.LogWarning("Enemy or Step Marker prefab is not assigned.");
            return;
        }

        Vector3 spawnPos = new Vector3(path[0].x, 0.5f, path[0].y);
        GameObject enemy = Object.Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        var mover = enemy.AddComponent<EnemyMover>();
        mover.stepMarkerPrefab = stepMarkerPrefab;
        mover.Init(gridFactory, path);
    }
}
