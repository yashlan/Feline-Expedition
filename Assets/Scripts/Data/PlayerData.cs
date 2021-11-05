using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : SingletonDontDestroy<PlayerData>
{
    public int HealPoint;
    public int DamageMelee;
    public float MoveSpeed;
    public bool CanDoubleJump = false;
    public bool CanDashing = false;


    void Start()
    {
        Load();
    }

    private void Load()
    {
        //load data dari playerprefs
        HealPoint = PlayerPrefs.GetInt(PlayerPrefsKey.HEALTHPOINT,    100);
        DamageMelee = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_MELEE, 500);
        MoveSpeed = PlayerPrefs.GetFloat(PlayerPrefsKey.MOVE_SPEED,    25);
        CanDoubleJump = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.CAN_DOUBLE_JUMP, 0));
        CanDashing = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.CAN_DASHING,        0));

        print("player stats loaded");
    }

    public static void Save(string prefsKey, int value)
    {
        PlayerPrefs.SetInt(prefsKey, value);
        Instance.Load();
    }

    public static void Save(string prefsKey, float value)
    {
        PlayerPrefs.SetFloat(prefsKey, value);
        Instance.Load();
    }

    public static void Save(string prefsKey, bool value)
    {
        PlayerPrefs.SetInt(prefsKey, Instance.IntValueOf(value));
        Instance.Load();
    }

    private int IntValueOf(bool val)
    {
        return val ? 1 : 0;
    }

    private bool BoolValueOf(int val)
    {
        return val != 0;
    }
}
