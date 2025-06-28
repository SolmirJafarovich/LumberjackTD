using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private DropTable dropTable;

    private HealthModel model;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;

    private void Awake()
    {
        model = new HealthModel(maxHealth);
        model.OnDeath += HandleDeath;
        model.OnDamaged += HandleDamage;

        // Сообщаем об изначальном здоровье
        OnHealthChanged?.Invoke(model.CurrentHealth, model.MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        model.TakeDamage(amount);
    }

    public void Heal(int amount)
    {
        model.Heal(amount);
        OnHealthChanged?.Invoke(model.CurrentHealth, model.MaxHealth);
    }

    private void HandleDamage(int amount)
    {
        Debug.Log($"{gameObject.name} took {amount} damage");
        OnHealthChanged?.Invoke(model.CurrentHealth, model.MaxHealth);
    }

    private void HandleDeath()
    {
        Debug.Log($"{gameObject.name} died");

        OnDied?.Invoke();

        if (dropTable != null)
        {
            var dropSystem = new DropSystem(dropTable);
            dropSystem.Drop(transform.position);
        }

        Destroy(gameObject);
    }

    public int CurrentHealth => model.CurrentHealth;
    public int MaxHealth => model.MaxHealth;
}
