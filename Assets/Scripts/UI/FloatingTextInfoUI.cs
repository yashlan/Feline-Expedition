using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextInfoUI : Singleton<FloatingTextInfoUI>
{
    static Text textInfo;

    // Start is called before the first frame update
    void Start()
    {
        textInfo = GetComponent<Text>();
        textInfo.enabled = false;
    }

    public static void Show(string msg, float timeToHide)
    {
        textInfo.enabled = true;
        textInfo.text = msg;
        Instance.Invoke(nameof(Hide), timeToHide);
    }

    private void Hide()
    {
        textInfo.text = null;
        textInfo.enabled = false;
    }
}
