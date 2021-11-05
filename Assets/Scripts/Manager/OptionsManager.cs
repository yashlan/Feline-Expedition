using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : SingletonDontDestroy<OptionsManager>
{
    public static KeyCode AttackMeleeKey { get; set; }
    public static KeyCode AttackThrowKey { get; set; }
    public static KeyCode RechargeKey { get; set; }
    public static KeyCode JumpKey { get; set; }
    public static KeyCode DashKey { get; set; }
    public static KeyCode LeftKey { get; set; }
    public static KeyCode RightKey { get; set; }

    void Start()
    {
        LoadInputKey();
    }

    void Update()
    {
        
    }

    public static void SaveNewKeyCode(string prefsKey, KeyCode defaultValue)
    {
        //save value
        PlayerPrefs.SetString(prefsKey, defaultValue.ToString());
        //kemudian load
        LoadInputKey();
    }

    private static void LoadInputKey()
    {
        LeftKey  = KeyCodeValueOf(PlayerPrefsKey.MOVE_LEFT,           KeyCode.LeftArrow);
        RightKey = KeyCodeValueOf(PlayerPrefsKey.MOVE_RIGHT,          KeyCode.RightArrow);
        JumpKey  = KeyCodeValueOf(PlayerPrefsKey.JUMP,                KeyCode.Space);
        DashKey  = KeyCodeValueOf(PlayerPrefsKey.DASH,                KeyCode.D);
        AttackMeleeKey  = KeyCodeValueOf(PlayerPrefsKey.ATTACK_MELEE, KeyCode.S);
        AttackThrowKey  = KeyCodeValueOf(PlayerPrefsKey.ATTACK_THROW, KeyCode.W);
        RechargeKey  = KeyCodeValueOf(PlayerPrefsKey.RECHARGE,        KeyCode.R);

        print("Input Key Was Loaded!");
    }

    private static KeyCode KeyCodeValueOf(string prefsKey, KeyCode defaultValue)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode),
            PlayerPrefs.GetString(prefsKey, defaultValue.ToString()));
    }
}
