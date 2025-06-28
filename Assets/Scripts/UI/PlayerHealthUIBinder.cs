using UnityEngine;

public class PlayerHealthUIBinder : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    private void Start()
    {
        health.OnHealthChanged += UpdateUI;
    }

    private void OnDestroy()
    {
        health.OnHealthChanged -= UpdateUI;
    }

    private void UpdateUI(int current, int max)
    {
        float percent = (float)current / max;
        UIManager.Instance?.UpdateHealth(percent);
    }
}
