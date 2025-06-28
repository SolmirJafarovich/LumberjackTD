using UnityEngine;
using UnityEngine.UI;

public class UIRingController : MonoBehaviour
{
    public enum RingMode { Clockwise, CounterClockwise }

    [SerializeField] private Image ringImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private RingMode currentMode = RingMode.Clockwise;
    private bool isVisible = false;

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (ringImage == null) ringImage = GetComponent<Image>();
        Hide();
    }


    public void Show()
    {
        if (!isVisible)
        {
            canvasGroup.alpha = 1f;
            isVisible = true;
        }
    }


    public void Hide()
    {
        canvasGroup.alpha = 0f;
        ringImage.fillAmount = 0f;
        isVisible = false;
    }

    public void SetMode(RingMode mode)
    {
        currentMode = mode;
        ringImage.fillClockwise = (mode == RingMode.Clockwise);
    }


    public void SetProgress(float value)
    {
        float clamped = Mathf.Clamp01(value);
        ringImage.fillAmount = clamped;
    }
}
