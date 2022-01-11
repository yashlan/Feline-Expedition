using System;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionsManager : SingletonDontDestroy<OptionsManager>
{
    public Texture2D cursorTexture;
    public RenderPipelineAsset[] renderPipelineAsset;

    #region KEYCODE
    private const string DEFAULT_KEY_LEFT        = "LeftArrow";
    private const string DEFAULT_KEY_RIGHT       = "RightArrow";
    private const string DEFAULT_KEY_JUMP        = "Z";
    private const string DEFAULT_KEY_DASH        = "C";
    private const string DEFAULT_KEY_MELEE       = "X";
    private const string DEFAULT_KEY_THROW       = "S";
    private const string DEFAULT_KEY_SELFHEAL    = "A";
    private const string DEFAULT_KEY_INTERACTION = "V";
    private const string DEFAULT_KEY_OPEN_MAP    = "D";

    public static KeyCode AttackMeleeKey { get; set; }
    public static KeyCode AttackThrowKey { get; set; }
    public static KeyCode SelfHealKey { get; set; }
    public static KeyCode JumpKey { get; set; }
    public static KeyCode DashKey { get; set; }
    public static KeyCode LeftKey { get; set; }
    public static KeyCode RightKey { get; set; }
    public static KeyCode InteractionKey { get; set; }
    public static KeyCode OpenMapKey { get; set; }

    #endregion

    #region VIDEO SETTING
    private const int DEFAULT_GRAPHIC_QUALITY = 3; //high
    private bool DEFAULT_DISPLAY_PLAYER_UI = true;
    private bool DEFAULT_DISPLAY_MODE_FULLSCREEN = true;

    public static bool IsFullScreen { get; set; }
    public static bool DisplayPlayerUI { get; set; }
    public static QualitySettings QualitySettings { get; set; }
    #endregion


    void Start()
    {
        SetupCursor();
        ShowMouseCursor();

        if (IsKeyCodeEmpty())
            SetDefaultButtonInput();
        else
            LoadButtonInputKey();

        if (IsVideoSettingEmpty())
            SetDefaultVideoSetting();
        else
            LoadVideoSetting();
    }

    #region KEYCODE
    public static void SaveNewKeyCode(string prefsKey, string newValue)
    {
        PlayerPrefs.SetString(prefsKey, newValue);
    }

    private static bool IsKeyCodeEmpty() =>
        PlayerPrefs.GetString(PlayerPrefsKey.MOVE_LEFT)    == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.MOVE_RIGHT)   == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.JUMP)         == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.DASH)         == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.ATTACK_MELEE) == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.ATTACK_THROW) == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.SELFHEAL)     == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.INTERACTION)  == string.Empty &&
        PlayerPrefs.GetString(PlayerPrefsKey.OPEN_MAP)     == string.Empty;

    public static void SetDefaultButtonInput()
    {
        PlayerPrefs.SetString(PlayerPrefsKey.MOVE_LEFT,    DEFAULT_KEY_LEFT);
        PlayerPrefs.SetString(PlayerPrefsKey.MOVE_RIGHT,   DEFAULT_KEY_RIGHT);
        PlayerPrefs.SetString(PlayerPrefsKey.JUMP,         DEFAULT_KEY_JUMP);
        PlayerPrefs.SetString(PlayerPrefsKey.DASH,         DEFAULT_KEY_DASH);
        PlayerPrefs.SetString(PlayerPrefsKey.ATTACK_MELEE, DEFAULT_KEY_MELEE);
        PlayerPrefs.SetString(PlayerPrefsKey.ATTACK_THROW, DEFAULT_KEY_THROW);
        PlayerPrefs.SetString(PlayerPrefsKey.SELFHEAL,     DEFAULT_KEY_SELFHEAL);
        PlayerPrefs.SetString(PlayerPrefsKey.INTERACTION,  DEFAULT_KEY_INTERACTION);
        PlayerPrefs.SetString(PlayerPrefsKey.OPEN_MAP,     DEFAULT_KEY_OPEN_MAP);

        LoadButtonInputKey();
    }

    private static void LoadButtonInputKey()
    {
        LeftKey        = KeyCodeValueOf(PlayerPrefsKey.MOVE_LEFT,    DEFAULT_KEY_LEFT);
        RightKey       = KeyCodeValueOf(PlayerPrefsKey.MOVE_RIGHT,   DEFAULT_KEY_RIGHT);
        JumpKey        = KeyCodeValueOf(PlayerPrefsKey.JUMP,         DEFAULT_KEY_JUMP);
        DashKey        = KeyCodeValueOf(PlayerPrefsKey.DASH,         DEFAULT_KEY_DASH);
        AttackMeleeKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_MELEE, DEFAULT_KEY_MELEE);
        AttackThrowKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_THROW, DEFAULT_KEY_THROW);
        SelfHealKey    = KeyCodeValueOf(PlayerPrefsKey.SELFHEAL,     DEFAULT_KEY_SELFHEAL);
        InteractionKey = KeyCodeValueOf(PlayerPrefsKey.INTERACTION,  DEFAULT_KEY_INTERACTION);
        OpenMapKey     = KeyCodeValueOf(PlayerPrefsKey.OPEN_MAP,     DEFAULT_KEY_OPEN_MAP);

        if (FindObjectsOfType<GetSpriteKeyCode>().Length > 0)
        {
            foreach (var imageTutorial in FindObjectsOfType<GetSpriteKeyCode>())
            {
                imageTutorial.UpdateKey();
            }
        }
    }

    private static KeyCode KeyCodeValueOf(string prefsKey, string defaultValue)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(prefsKey, defaultValue));
    }

    #endregion KEYCODE


    #region VIDEO SETTING

    private bool IsVideoSettingEmpty() =>
        !PlayerPrefs.HasKey(PlayerPrefsKey.DISPLAY_MODE_FULLSCREEN) &&
        !PlayerPrefs.HasKey(PlayerPrefsKey.DISPLAY_PLAYER_UI)       &&
        !PlayerPrefs.HasKey(PlayerPrefsKey.GRAPHIC_QUALITY);


    private void SetDefaultVideoSetting()
    {
        PlayerPrefs.SetInt(
            PlayerPrefsKey.DISPLAY_MODE_FULLSCREEN,
            IntValueOf(DEFAULT_DISPLAY_MODE_FULLSCREEN));

        PlayerPrefs.SetInt(
                PlayerPrefsKey.DISPLAY_PLAYER_UI,
                IntValueOf(DEFAULT_DISPLAY_PLAYER_UI));

        PlayerPrefs.SetInt(
                    PlayerPrefsKey.GRAPHIC_QUALITY,
                    DEFAULT_GRAPHIC_QUALITY);

        LoadVideoSetting();
    }

    public void LoadVideoSetting()
    {
        IsFullScreen = BoolValueOf(
            PlayerPrefs.GetInt(
                PlayerPrefsKey.DISPLAY_MODE_FULLSCREEN, 
                IntValueOf(DEFAULT_DISPLAY_MODE_FULLSCREEN)));

        DisplayPlayerUI = BoolValueOf(
            PlayerPrefs.GetInt(
                PlayerPrefsKey.DISPLAY_PLAYER_UI, 
                IntValueOf(DEFAULT_DISPLAY_PLAYER_UI)));

        QualitySettings.SetQualityLevel(
            PlayerPrefs.GetInt(
                    PlayerPrefsKey.GRAPHIC_QUALITY,
                    DEFAULT_GRAPHIC_QUALITY));
    }

    public static void SaveVideoSetting(string prefsKey, bool value)
    {
        PlayerPrefs.SetInt(prefsKey, Instance.IntValueOf(value));
        Instance.LoadVideoSetting();
    }

    public static void SaveGraphicsSetting(string prefsKey, int value)
    {
        PlayerPrefs.SetInt(prefsKey, value);
        Instance.LoadVideoSetting();
    }

    public static void SaveDisplayPlayerUI(string prefsKey, int value)
    {
        PlayerPrefs.SetInt(prefsKey, value);
        Instance.LoadVideoSetting();
    }

    #endregion VIDEO SETTING

    #region MOUSE CURSOR

    private void SetupCursor()
    {
        Cursor.SetCursor(Instance.cursorTexture, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void HideMouseCursor()
    {
#if !UNITY_EDITOR
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    public static void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
#endregion CURSOR MOUSE

#region UTILITY
    private int IntValueOf(bool val) => val ? 1 : 0;
    private bool BoolValueOf(int val) => val != 0;
#endregion

}
