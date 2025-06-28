using UnityEngine;
using UnityEngine.UI;

public class UIRingController : MonoBehaviour
{
    [SerializeField] private Image ringImage;
    [SerializeField] private CanvasGroup canvasGroup;

    public void Show() => canvasGroup.alpha = 1;
    public void Hide()
    {
        canvasGroup.alpha = 0;
        ringImage.fillAmount = 0f;
    }

    public void SetProgress(float value)
    {
        ringImage.fillAmount = Mathf.Clamp01(value);
    }
}
