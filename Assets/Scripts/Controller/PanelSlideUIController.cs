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

    public void FadeIn(Action OnFadeIn, bool ThenFadeOut)
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeIn(OnFadeIn, ThenFadeOut));
    }

    public void FadeIn(Action OnFadeIn, float invokeActionTime)
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeIn(OnFadeIn, invokeActionTime));
    }

    void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeIn(Action OnFadeIn, bool ThenFadeOut)
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
            if(ThenFadeOut)
                Invoke(nameof(FadeOut), 1f);
        }
    }

    IEnumerator StartFadeIn(Action OnFadeIn, float invokeActionTime)
    {
        while (image.color.a != 1)
        {
            var i = 0.025f;
            image.color = new Color(0, 0, 0, image.color.a + i);
            yield return new WaitForSeconds(0.05f);
        }

        if (image.color.a == 1)
        {
            yield return new WaitForSeconds(invokeActionTime);
            OnFadeIn?.Invoke();
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
