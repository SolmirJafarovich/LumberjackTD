using UnityEngine;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMPro.TextMeshProUGUI messageText;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
