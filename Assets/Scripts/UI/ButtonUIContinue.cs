using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIContinue : MonoBehaviour
{
    Button button;
    Text button_text;

    Color32 enabled_color = new Color32(255, 255, 255, 255);
    Color32 disabled_color = new Color32(191, 191, 191, 150);

    void Awake()
    {
        button = GetComponent<Button>();
        button_text = GetComponentInChildren<Text>();

        button.interactable = IsInteractable();
        button_text.color = IsInteractable() ? enabled_color : disabled_color;
    }

    private bool IsInteractable() => PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE);
}
