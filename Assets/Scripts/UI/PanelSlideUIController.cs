using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelSlideUIController : Singleton<PanelSlideUIController>
{
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(0, 0, 0, 1);

        StartCoroutine(StartFadeOut());
    }

    public void FadeIn(Action OnFadeIn)
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeIn(OnFadeIn));
    }

    void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeIn(Action OnFadeIn)
    {
        while (image.color.a != 1)
        {
            var i = 0.025f;
            image.color = new Color(0, 0, 0, image.color.a + i);
            yield return new WaitForSeconds(0.05f);
        }

        if(image.color.a == 1)
        {
            OnFadeIn?.Invoke();
            Invoke(nameof(FadeOut), 1f);
        }
    }

    IEnumerator StartFadeOut()
    {
        while(image.color.a > 0)
        {
            var i = 0.025f;
            image.color = new Color(0, 0, 0, image.color.a - i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
