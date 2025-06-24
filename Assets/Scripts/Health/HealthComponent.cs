using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private DropTable dropTable;
    private HealthModel model;

    private void Awake()
    {
        model = new HealthModel(maxHealth);
        model.OnDeath += HandleDeath;
        model.OnDamaged += HandleDamage;
    }

    public void TakeDamage(int amount)
    {
        model.TakeDamage(amount);
    }

    private void HandleDamage(int amount)
    {
        // Можно добавить эффект: вспышку, звук, UI и т.п.
        Debug.Log($"{gameObject.name} took {amount} damage");
    }

    private void HandleDeath()
    {
        Debug.Log($"{gameObject.name} died");

        if (dropTable != null)
        {
            var dropSystem = new DropSystem(dropTable);
            dropSystem.Drop(transform.position);
        }

        Destroy(gameObject); // Или пуллинг
    }

    public void Heal(int amount)
    {
        model.Heal(amount);
    }

    public int CurrentHealth => model.CurrentHealth;
    public int MaxHealth => model.MaxHealth;
}
