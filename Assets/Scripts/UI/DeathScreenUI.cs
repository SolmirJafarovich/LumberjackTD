using UnityEngine;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMPro.TextMeshProUGUI messageText;

    private float originalTimeScale = 1f;

    private void Awake()
    {
        panel.SetActive(false);
    }

    public void Show(string message)
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        panel.SetActive(true);
        messageText.text = message;
    }

    public void Hide()
    {

        Time.timeScale = originalTimeScale;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        panel.SetActive(false);
    }
}
