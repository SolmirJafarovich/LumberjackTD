using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private List<Vector2Int> path;
    private int currentIndex;
    private float speed = 2f;
    private System.Action<GameObject> onFinish;

    public void Init(List<Vector2Int> path, System.Action<GameObject> onFinish)
    {
        this.path = path;
        this.onFinish = onFinish;
        currentIndex = 0;
        transform.position = GridToWorld(path[0]);
    }

    void Update()
    {
        if (currentIndex >= path.Count) return;

        Vector3 target = GridToWorld(path[currentIndex]);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                onFinish?.Invoke(gameObject); // вызываем коллбек уничтожения
            }
        }
    }

    private Vector3 GridToWorld(Vector2Int pos) => new(pos.x, 1, pos.y);
}
