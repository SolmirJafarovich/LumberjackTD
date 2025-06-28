using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TMPro.TextMeshProUGUI resourceText;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void UpdateResource(string resourceType, int amount)
    {
        resourceText.text = $"{resourceType}: {amount}";
    }

    public void UpdateHealth(float percent)
    {
        if (healthBar != null)
            healthBar.value = percent;
    }
}
