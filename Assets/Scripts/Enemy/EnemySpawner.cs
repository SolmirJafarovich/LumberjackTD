using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private readonly GameObject enemyPrefab;
    private readonly GameObject stepMarkerPrefab;
    private readonly GridFactory gridFactory;
    private readonly List<GameObject> spawnedEnemies = new();

    public EnemySpawner(GameObject enemyPrefab, GameObject stepMarkerPrefab, GridFactory gridFactory)
    {
        this.enemyPrefab = enemyPrefab;
        this.stepMarkerPrefab = stepMarkerPrefab;
        this.gridFactory = gridFactory;
    }

    public void SpawnAndRun(List<Vector2Int> path)
    {
        ClearPrevious();

        GameObject enemy = Object.Instantiate(enemyPrefab, GridToWorld(path[0]), Quaternion.identity);
        spawnedEnemies.Add(enemy);

        enemy.AddComponent<EnemyMover>().Init(path, OnReachedEnd);

        foreach (var step in path)
        {
            var marker = Object.Instantiate(stepMarkerPrefab, GridToWorld(step), Quaternion.identity);
            Object.Destroy(marker, 2.5f); // удаление маркеров через 1.5 сек
        }
    }

    private void ClearPrevious()
    {
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
                Object.Destroy(enemy);
        }
        spawnedEnemies.Clear();
    }

    private Vector3 GridToWorld(Vector2Int pos) => new(pos.x, 1, pos.y);

    private void OnReachedEnd(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        Object.Destroy(enemy);
    }
}
