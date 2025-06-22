using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float shootInterval = 1f;
    [SerializeField] private int damage = 25;
    [SerializeField] private string targetTag = "Enemy";

    private float timer;
    private ITargetFilter targetFilter;

    private void Awake()
    {
        targetFilter = new TagTargetFilter(targetTag);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            timer = 0f;
            TryShoot();
        }
    }

    private void TryShoot()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (var hit in hits)
        {
            if (!targetFilter.IsTargetValid(hit.gameObject)) continue;

            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage(damage);
                Debug.DrawLine(transform.position, hit.transform.position, Color.red, 0.2f);
                break;
            }
        }
    }
}
