using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRuneUI : Singleton<PlayerRuneUI>
{
    public GameObject[] images;

    public static void Show()
    {
        foreach (var item in Instance.images)
        {
            item.SetActive(true);
        }
    }

    public static void Hide()
    {
        foreach (var item in Instance.images)
        {
            item.SetActive(false);
        }
    }
}
