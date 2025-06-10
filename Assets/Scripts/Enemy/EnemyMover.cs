using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject stepMarkerPrefab;

    private List<Vector2Int> path;
    private int currentIndex = 0;
    private GridFactory gridFactory;

    public void Init(GridFactory manager, List<Vector2Int> path)
    {
        this.gridFactory = manager;
        this.path = path;
        this.currentIndex = 0;

        // Переместить к стартовой позиции
        if (path != null && path.Count > 0)
        {
            transform.position = GridToWorld(path[0]);
        }

        // Запускаем движение
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        while (currentIndex < path.Count)
        {
            Vector3 targetPos = GridToWorld(path[currentIndex]);

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Поставить "след"
            if (stepMarkerPrefab != null)
            {
                Instantiate(stepMarkerPrefab, targetPos + Vector3.up * 0.01f, Quaternion.identity);
            }

            currentIndex++;
            yield return new WaitForSeconds(0.05f); // Пауза между шагами
        }

        Debug.Log("Достиг цели.");
    }

    private Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x, 0.5f, gridPos.y);
    }
}
