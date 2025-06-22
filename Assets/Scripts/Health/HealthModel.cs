using System;

public class HealthModel
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }

    public bool IsAlive => CurrentHealth > 0;

    public event Action OnDeath;
    public event Action<int> OnDamaged;

    public HealthModel(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;

        CurrentHealth -= amount;
        OnDamaged?.Invoke(amount);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (!IsAlive) return;

        CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth);
    }
}
