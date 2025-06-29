using UnityEngine;

public class PlayerHealthUIBinder : MonoBehaviour
{
    private HealthComponent health;

    public void Bind(HealthComponent newHealth)
    {
        if (health != null)
        {
            health.OnHealthChanged -= UpdateUI;
        }

        health = newHealth;
        health.OnHealthChanged += UpdateUI;

        // Обновим UI сразу
        UpdateUI(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnHealthChanged -= UpdateUI;
        }
    }

    private void UpdateUI(int current, int max)
    {
        float percent = (float)current / max;
        UIManager.Instance?.UpdateHealth(percent);
    }
}
