using System;
using UnityEngine;

public class OptionsManager : SingletonDontDestroy<OptionsManager>
{
    #region KEYCODE
    private const string DEFAULT_KEY_LEFT        = "LeftArrow";
    private const string DEFAULT_KEY_RIGHT       = "RightArrow";
    private const string DEFAULT_KEY_JUMP        = "Z";
    private const string DEFAULT_KEY_DASH        = "C";
    private const string DEFAULT_KEY_MELEE       = "X";
    private const string DEFAULT_KEY_THROW       = "S";
    private const string DEFAULT_KEY_RECHARGE    = "A";
    private const string DEFAULT_KEY_INTERACTION = "V";
    private const string DEFAULT_KEY_OPEN_MAP    = "D";

    public static KeyCode AttackMeleeKey { get; set; }
    public static KeyCode AttackThrowKey { get; set; }
    public static KeyCode RechargeKey { get; set; }
    public static KeyCode JumpKey { get; set; }
    public static KeyCode DashKey { get; set; }
    public static KeyCode LeftKey { get; set; }
    public static KeyCode RightKey { get; set; }
    public static KeyCode InteractionKey { get; set; }
    public static KeyCode OpenMapKey { get; set; }

    #endregion

    void Start()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey.FIRST_PLAY))
        {
            SetDefaultButtonInput();
            PlayerPrefs.SetInt(PlayerPrefsKey.FIRST_PLAY, 1);
        }
        else
        {
            LoadInputKey();
            //HideMouseCursor();
        }
    }

    #region KEYCODE
    public static void SaveNewKeyCode(string prefsKey, string newValue)
    {
        PlayerPrefs.SetString(prefsKey, newValue);
    }

    public static void SetDefaultButtonInput()
    {
        PlayerPrefs.SetString(PlayerPrefsKey.MOVE_LEFT,    DEFAULT_KEY_LEFT);
        PlayerPrefs.SetString(PlayerPrefsKey.MOVE_RIGHT,   DEFAULT_KEY_RIGHT);
        PlayerPrefs.SetString(PlayerPrefsKey.JUMP,         DEFAULT_KEY_JUMP);
        PlayerPrefs.SetString(PlayerPrefsKey.DASH,         DEFAULT_KEY_DASH);
        PlayerPrefs.SetString(PlayerPrefsKey.ATTACK_MELEE, DEFAULT_KEY_MELEE);
        PlayerPrefs.SetString(PlayerPrefsKey.ATTACK_THROW, DEFAULT_KEY_THROW);
        PlayerPrefs.SetString(PlayerPrefsKey.RECHARGE,     DEFAULT_KEY_RECHARGE);
        PlayerPrefs.SetString(PlayerPrefsKey.INTERACTION,  DEFAULT_KEY_INTERACTION);
        PlayerPrefs.SetString(PlayerPrefsKey.OPEN_MAP,     DEFAULT_KEY_OPEN_MAP);

        LoadInputKey();
    }

    private static void LoadInputKey()
    {
        LeftKey        = KeyCodeValueOf(PlayerPrefsKey.MOVE_LEFT,    DEFAULT_KEY_LEFT);
        RightKey       = KeyCodeValueOf(PlayerPrefsKey.MOVE_RIGHT,   DEFAULT_KEY_RIGHT);
        JumpKey        = KeyCodeValueOf(PlayerPrefsKey.JUMP,         DEFAULT_KEY_JUMP);
        DashKey        = KeyCodeValueOf(PlayerPrefsKey.DASH,         DEFAULT_KEY_DASH);
        AttackMeleeKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_MELEE, DEFAULT_KEY_MELEE);
        AttackThrowKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_THROW, DEFAULT_KEY_THROW);
        RechargeKey    = KeyCodeValueOf(PlayerPrefsKey.RECHARGE,     DEFAULT_KEY_RECHARGE);
        InteractionKey = KeyCodeValueOf(PlayerPrefsKey.INTERACTION,  DEFAULT_KEY_INTERACTION);
        OpenMapKey     = KeyCodeValueOf(PlayerPrefsKey.OPEN_MAP,     DEFAULT_KEY_OPEN_MAP);
    }

    private static KeyCode KeyCodeValueOf(string prefsKey, string defaultValue)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(prefsKey, defaultValue));
    }

    #endregion KEYCODE

    #region MOUSE

    private void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion MOUSE
}
