using System;
using UnityEngine;

public class OptionsManager : SingletonDontDestroy<OptionsManager>
{
    #region KEYCODE
    private const string DEFAULT_KEY_LEFT     = "LeftArrow";
    private const string DEFAULT_KEY_RIGHT    = "RightArrow";
    private const string DEFAULT_KEY_JUMP     = "Space";
    private const string DEFAULT_KEY_DASH     = "C";
    private const string DEFAULT_KEY_MELEE    = "Z";
    private const string DEFAULT_KEY_THROW    = "T";
    private const string DEFAULT_KEY_RECHARGE = "A";

    public static KeyCode AttackMeleeKey { get; set; }
    public static KeyCode AttackThrowKey { get; set; }
    public static KeyCode RechargeKey { get; set; }
    public static KeyCode JumpKey { get; set; }
    public static KeyCode DashKey { get; set; }
    public static KeyCode LeftKey { get; set; }
    public static KeyCode RightKey { get; set; }
    #endregion

    void Start()
    {
        LoadInputKey();
    }

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

        LoadInputKey();
    }

    private static void LoadInputKey()
    {
        LeftKey = KeyCodeValueOf(PlayerPrefsKey.MOVE_LEFT,           DEFAULT_KEY_LEFT);
        RightKey = KeyCodeValueOf(PlayerPrefsKey.MOVE_RIGHT,         DEFAULT_KEY_RIGHT);
        JumpKey = KeyCodeValueOf(PlayerPrefsKey.JUMP,                DEFAULT_KEY_JUMP);
        DashKey = KeyCodeValueOf(PlayerPrefsKey.DASH,                DEFAULT_KEY_DASH);
        AttackMeleeKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_MELEE, DEFAULT_KEY_MELEE);
        AttackThrowKey = KeyCodeValueOf(PlayerPrefsKey.ATTACK_THROW, DEFAULT_KEY_THROW);
        RechargeKey = KeyCodeValueOf(PlayerPrefsKey.RECHARGE,        DEFAULT_KEY_RECHARGE);
    }

    private static KeyCode KeyCodeValueOf(string prefsKey, string defaultValue)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(prefsKey, defaultValue));
    }
}
