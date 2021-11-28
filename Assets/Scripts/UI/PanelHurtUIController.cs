
using UnityEngine;
using UnityEngine.UI;

public class PanelHurtUIController : Singleton<PanelHurtUIController>
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    public void Show()
    {
        image.enabled = true;
    }

    public void Hide()
    {
        image.enabled = false;
    }
}
