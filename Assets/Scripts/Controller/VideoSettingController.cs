using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VideoSettingController : MonoBehaviour
{
    public RenderPipelineAsset[] renderPipelineAsset;

    public Button buttonDisplayModeLeft;
    public Button buttonDisplayModeRight;

    public Button buttonGraphicsLeft;
    public Button buttonGraphicsRight;

    public Button buttonDisplayPlayerUILeft;
    public Button buttonDisplayPlayerUIRight;

    public Text textDisplayMode;
    public Text textGraphics;
    public Text textDisplayPlayerUI;

    void Start()
    {
        textDisplayMode.text = OptionsManager.IsFullScreen ? "fullscreen" : "windowed";

        var index = PlayerPrefs.GetInt(PlayerPrefsKey.GRAPHIC_QUALITY);
        textGraphics.text = QualitySettings.names[index];

        textDisplayPlayerUI.text = OptionsManager.DisplayPlayerUI ? "on" : "off";

        buttonDisplayModeLeft.onClick.AddListener(ChangeValueDisplayMode);
        buttonDisplayModeRight.onClick.AddListener(ChangeValueDisplayMode);

        buttonGraphicsLeft.onClick.AddListener(() => ChangeValueGraphicsQuality(false));
        buttonGraphicsRight.onClick.AddListener(() => ChangeValueGraphicsQuality(true));

        buttonDisplayPlayerUILeft.onClick.AddListener(ChangeDisplayPlayerUIValue);
        buttonDisplayPlayerUIRight.onClick.AddListener(ChangeDisplayPlayerUIValue);
    }

    private void ChangeValueDisplayMode()
    {
        Screen.fullScreen = !Screen.fullScreen;

        Invoke(nameof(SaveDisplayModeValue), 0.5f);
    }

    private void SaveDisplayModeValue()
    {
        OptionsManager.SaveVideoSetting(
            PlayerPrefsKey.DISPLAY_MODE_FULLSCREEN, 
            Screen.fullScreen);

        textDisplayMode.text = 
            OptionsManager.IsFullScreen ? 
            "fullscreen" : "windowed";
    }

    private void ChangeValueGraphicsQuality(bool Increment)
    {
        var i = QualitySettings.GetQualityLevel();

        if (Increment) i++;
        else i--;

        if (i < 0) i = 5;
        if (i > 5) i = 0;

        QualitySettings.SetQualityLevel(i);
        //QualitySettings.renderPipeline = renderPipelineAsset[i];

        textGraphics.text = QualitySettings.names[i];
        OptionsManager.SaveGraphicsSetting(PlayerPrefsKey.GRAPHIC_QUALITY, i);
    }

    private void ChangeDisplayPlayerUIValue()
    {
        OptionsManager.DisplayPlayerUI = !OptionsManager.DisplayPlayerUI;

        OptionsManager.SaveDisplayPlayerUI(
            PlayerPrefsKey.DISPLAY_PLAYER_UI,
            IntValueOf(OptionsManager.DisplayPlayerUI));

        textDisplayPlayerUI.text = OptionsManager.DisplayPlayerUI ? "on" : "off";

        if (PlayerRuneUI.Instance == null)
            return;

        if (OptionsManager.DisplayPlayerUI)
            PlayerRuneUI.Show();
        else
            PlayerRuneUI.Hide();
    }

    private int IntValueOf(bool val) => val ? 1 : 0;
}
