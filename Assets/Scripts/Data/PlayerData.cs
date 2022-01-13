using UnityEngine;

public class PlayerData : SingletonDontDestroy<PlayerData>
{
    [Header("Additional Stats Attribute")]
    [SerializeField]
    private int _healthPointExtra;
    [SerializeField]
    private int _manaPointExtra;
    [SerializeField]
    private int _damageReductionExtra;
    [SerializeField]
    private int _damageMeleeExtra;
    [SerializeField]
    private int _damageMagicExtra;
    [SerializeField]
    private int _manaRegenExtra;


    [Header("Total Stats Attribute")]
    [SerializeField]
    private int _healthPoint;
    [SerializeField]
    private float _manaPoint;
    [SerializeField]
    private int _damageReduction;
    [SerializeField]
    private int _damageMelee;
    [SerializeField]
    private int _damageMagic;
    [SerializeField]
    private int _manaRegen;

    [Header("Shop Attribute")]
    [SerializeField]
    private int _totalRuneSlotUsed;
    [SerializeField]
    private int _coins;

    [Header("Purchased Items")]
    [SerializeField]
    private bool _isBeastWasPurchased;
    [SerializeField]
    private bool _isDisorderWasPurchased;
    [SerializeField]
    private bool _isIlusionWasPurchased;
    [SerializeField]
    private bool _isTruthWasPurchased;
    [SerializeField]
    private bool _isHarmonyWasPurchased;
    [SerializeField]
    private bool _isSpiritWasPurchased;

    [Header("Unlocked Rune")]
    [SerializeField]
    private bool _isWaterSpearUnlocked;
    [SerializeField]
    private bool _isInvincibleShieldUnlocked;

    [Header("Unlocked Map")]
    [SerializeField]
    private bool _isMapUnlocked;

    [Header("Passive Rune Equip")]
    [SerializeField]
    private bool _isBeastEquip;
    [SerializeField]
    private bool _isDisorderEquip;
    [SerializeField]
    private bool _isIlusionEquip;
    [SerializeField]
    private bool _isTruthEquip;
    [SerializeField]
    private bool _isHarmonyEquip;
    [SerializeField]
    private bool _isSpiritRuneEquip;

    [Header("Active Rune Equip")]
    [SerializeField]
    private bool _isWaterSpearEquip;
    [SerializeField]
    private bool _isInvincibleShieldEquip;

    [Header("CheckPoint")]
    [SerializeField]
    private string _lastScene;
    [SerializeField]
    private string _lastCheckPoint;

    [Header("Talk NPC Session")]
    [SerializeField]
    private int _npcGerrinTalkSession = 1;
    [SerializeField]
    private int _npcGwynnTalkSession  = 1;
    [SerializeField]
    private int _npcRoccaTalkSession  = 1;

    #region GETTER SETTER
    public static int HealthPoint
    { 
        get => Instance._healthPoint; 
        set => Instance._healthPoint = value; 
    }

    public static int DamageMelee 
    {
        get => Instance._damageMelee; 
        set => Instance._damageMelee = value; 
    }

    public static float ManaPoint 
    { 
        get => Instance._manaPoint; 
        set => Instance._manaPoint = value; 
    }

    public static int DamageReduction 
    { 
        get => Instance._damageReduction; 
        set => Instance._damageReduction = value;
    }

    public static int DamageMagic
    {
        get => Instance._damageMagic; 
        set => Instance._damageMagic = value;
    }

    public static int ManaRegen
    {
        get => Instance._manaRegen; 
        set => Instance._manaRegen = value;
    }

    public static string LastScene 
    { 
        get => Instance._lastScene; 
        set => Instance._lastScene = value;
    }

    public static string LastCheckPoint 
    {
        get => Instance._lastCheckPoint;
        set => Instance._lastCheckPoint = value;
    }

    public static bool IsWaterSpearUnlocked 
    { 
        get => Instance._isWaterSpearUnlocked; 
        set => Instance._isWaterSpearUnlocked = value; 
    }

    public static bool IsInvincibleShieldUnlocked 
    { 
        get => Instance._isInvincibleShieldUnlocked; 
        set => Instance._isInvincibleShieldUnlocked = value; 
    }

    public static bool IsWaterSpearEquip 
    { 
        get => Instance._isWaterSpearEquip;
        set => Instance._isWaterSpearEquip = value;
    }

    public static bool IsInvincibleShieldEquip 
    { 
        get => Instance._isInvincibleShieldEquip; 
        set => Instance._isInvincibleShieldEquip = value;
    }

    public static int Coins 
    {
        get => Instance._coins; 
        set => Instance._coins = value; 
    }

    public static int HealthPointExtra 
    { 
        get => Instance._healthPointExtra; 
        set => Instance._healthPointExtra = value; 
    }

    public static int ManaPointExtra
    {
        get => Instance._manaPointExtra;
        set => Instance._manaPointExtra = value;
    }

    public static int NpcGerrinTalkSession 
    { 
        get => Instance._npcGerrinTalkSession; 
        set => Instance._npcGerrinTalkSession = value; 
    }

    public static int NpcGwynnTalkSession 
    { 
        get => Instance._npcGwynnTalkSession; 
        set => Instance._npcGwynnTalkSession = value; 
    }

    public static int NpcRoccaTalkSession 
    {
        get => Instance._npcRoccaTalkSession; 
        set => Instance._npcRoccaTalkSession = value;
    }

    public static bool IsMapUnlocked 
    { 
        get => Instance._isMapUnlocked;
        set => Instance._isMapUnlocked = value;
    }

    public static bool IsBeastWasPurchased 
    { 
        get => Instance._isBeastWasPurchased; 
        set => Instance._isBeastWasPurchased = value; 
    }

    public static bool IsDisorderWasPurchased 
    { 
        get => Instance._isDisorderWasPurchased; 
        set => Instance._isDisorderWasPurchased = value; 
    }

    public static bool IsIlusionWasPurchased
    {
        get => Instance._isIlusionWasPurchased; 
        set => Instance._isIlusionWasPurchased = value;
    }

    public static bool IsTruthWasPurchased
    {
        get => Instance._isTruthWasPurchased; 
        set => Instance._isTruthWasPurchased = value;
    }

    public static bool IsHarmonyWasPurchased
    {
        get => Instance._isHarmonyWasPurchased;
        set => Instance._isHarmonyWasPurchased = value;
    }

    public static bool IsSpiritWasPurchased
    {
        get => Instance._isSpiritWasPurchased; 
        set => Instance._isSpiritWasPurchased = value;
    }

    public static bool IsBeastEquip 
    {
        get => Instance._isBeastEquip;
        set => Instance._isBeastEquip = value; 
    }

    public static bool IsDisorderEquip 
    { 
        get => Instance._isDisorderEquip;
        set => Instance._isDisorderEquip = value; 
    }

    public static bool IsIlusionEquip 
    { 
        get => Instance._isIlusionEquip;
        set => Instance._isIlusionEquip = value;
    }

    public static bool IsTruthEquip
    {
        get => Instance._isTruthEquip; 
        set => Instance._isTruthEquip = value;
    }

    public static bool IsHarmonyEquip
    {
        get => Instance._isHarmonyEquip; 
        set => Instance._isHarmonyEquip = value;
    }

    public static bool IsSpiritRuneEquip
    {
        get => Instance._isSpiritRuneEquip; 
        set => Instance._isSpiritRuneEquip = value;
    }

    #endregion

    public static bool IsInvincibleShieldUsed() => IsInvincibleShieldUnlocked && IsInvincibleShieldEquip;
    public static bool IsWaterSpearUsed() => IsWaterSpearUnlocked && IsWaterSpearEquip;


    #region DEFAULT STATS
    public const int DEFAULT_HEALTHPOINT      = 30;
    public const int DEFAULT_MANAPOINT        = 100;
    public const int DEFAULT_DAMAGE_MELEE     = 10;
    public const int DEFAULT_DAMAGE_MAGIC     = 7;
    public const int DEFAULT_DAMAGE_REDUCTION = 0;
    public const int DEFAULT_MANA_REGEN       = 3;
    #endregion



    void Start()
    {
        Load();
    }

    #region DELETE

    public static void Delete(string prefsKey)
    {
        PlayerPrefs.DeleteKey(prefsKey);
        Instance.Load();
    }

    #endregion

    #region LOAD

    private void Load()
    {
        _isWaterSpearEquip = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.WATER_SPEAR_EQUIP, 0));
        _isInvincibleShieldEquip = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.INVINCIBLE_SHIELD_EQUIP, 0));


        _isWaterSpearUnlocked = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.WATER_SPEAR, 0));
        _isInvincibleShieldUnlocked = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.INVINCIBLE_SHIELD, 0));
        _isMapUnlocked = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.UNLOCKED_MAP, 0));

        #region NPC
        _npcGerrinTalkSession = PlayerPrefs.GetInt(PlayerPrefsKey.GERRIN_TALK_SESSION, _npcGerrinTalkSession);
        _npcGwynnTalkSession  = PlayerPrefs.GetInt(PlayerPrefsKey.GWYNN_TALK_SESSION,  _npcGwynnTalkSession);
        _npcRoccaTalkSession  = PlayerPrefs.GetInt(PlayerPrefsKey.ROCCA_TALK_SESSION,  _npcRoccaTalkSession);
        #endregion

        #region SHOP
        _coins = PlayerPrefs.GetInt(PlayerPrefsKey.COIN,                             _coins);
        _totalRuneSlotUsed = PlayerPrefs.GetInt(PlayerPrefsKey.TOTAL_RUNE_SLOT_USED, _totalRuneSlotUsed);

        _isBeastWasPurchased    = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.BEAST_PURCHASED,    0));
        _isDisorderWasPurchased = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.DISORDER_PURCHASED, 0));
        _isIlusionWasPurchased  = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.ILUSION_PURCHASED,  0));
        _isTruthWasPurchased    = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.TRUTH_PURCHASED,    0));
        _isHarmonyWasPurchased  = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.HARMONY_PURCHASED,  0));
        _isSpiritWasPurchased   = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.SPIRIT_PURCHASED,   0));

        _isBeastEquip      = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.BEAST_EQUIP,    0));
        _isDisorderEquip   = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.DISORDER_EQUIP, 0));
        _isIlusionEquip    = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.ILUSION_EQUIP,  0));
        _isTruthEquip      = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.TRUTH_EQUIP,    0));
        _isHarmonyEquip    = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.HARMONY_EQUIP,  0));
        _isSpiritRuneEquip = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.SPIRIT_EQUIP,   0));

        #endregion

        _healthPointExtra = PlayerPrefs.GetInt(PlayerPrefsKey.HEALTHPOINT_EXTRA,          _healthPointExtra);
        _manaPointExtra   = PlayerPrefs.GetInt(PlayerPrefsKey.MANAPOINT_EXTRA,            _manaPointExtra);
        _damageMeleeExtra = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_MELEE_EXTRA,         _damageMeleeExtra);
        _damageMagicExtra = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_MAGIC_EXTRA,         _damageMagicExtra);
        _damageReductionExtra = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_REDUCTION_EXTRA, _damageReductionExtra);
        _manaRegenExtra = PlayerPrefs.GetInt(PlayerPrefsKey.MANA_REGEN_EXTRA,             _manaRegenExtra);

        _healthPoint = PlayerPrefs.GetInt(PlayerPrefsKey.HEALTHPOINT,          DEFAULT_HEALTHPOINT      + _healthPointExtra);
        _manaPoint   = PlayerPrefs.GetFloat(PlayerPrefsKey.MANAPOINT,          DEFAULT_MANAPOINT        + _manaPointExtra);
        _damageMelee = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_MELEE,         DEFAULT_DAMAGE_MELEE     + _damageMeleeExtra);
        _damageMagic = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_MAGIC,         DEFAULT_DAMAGE_MAGIC     + _damageMagicExtra);
        _damageReduction = PlayerPrefs.GetInt(PlayerPrefsKey.DAMAGE_REDUCTION, DEFAULT_DAMAGE_REDUCTION + _damageReductionExtra);
        _manaRegen = PlayerPrefs.GetInt(PlayerPrefsKey.MANA_REGEN,             DEFAULT_MANA_REGEN       + _manaRegenExtra);

        _lastScene = PlayerPrefs.GetString(PlayerPrefsKey.LAST_SCENE,           _lastScene);
        _lastCheckPoint = PlayerPrefs.GetString(PlayerPrefsKey.LAST_CHECKPOINT, _lastCheckPoint);
    }
    #endregion

    #region SAVE
    public static void Save(string prefsKey, int value)
    {
        PlayerPrefs.SetInt(prefsKey, value);
        Instance.Load();
    }

    public static void Save(string prefsKey, bool value)
    {
        PlayerPrefs.SetInt(prefsKey, Instance.IntValueOf(value));
        Instance.Load();
    }

    public static void Save(string prefsKey, float value)
    {
        PlayerPrefs.SetFloat(prefsKey, value);
        Instance.Load();
    }

    public static void Save(string prefsKey, string value)
    {
        PlayerPrefs.SetString(prefsKey, value);
        Instance.Load();
    }
    #endregion

    #region UTILITY
    private int IntValueOf(bool val) => val ? 1 : 0;
    private bool BoolValueOf(int val) => val != 0;
    #endregion

    public static void SetDefaultValue()
    {
        var player = PlayerController.Instance;

        Instance._healthPoint = DEFAULT_HEALTHPOINT + Instance._healthPointExtra;
        Instance._manaPoint   = DEFAULT_MANAPOINT + Instance._manaPointExtra;
        Instance._coins       = 0;

        player.HealthPoint = Instance._healthPoint;
        player.ManaPoint = Instance._manaPoint;
        player.Coins = Instance._coins;

        Save(PlayerPrefsKey.HEALTHPOINT, player.HealthPoint);
        Save(PlayerPrefsKey.MANAPOINT, player.ManaPoint);
        Save(PlayerPrefsKey.COIN, player.Coins);
    }

    public void OnRuneEquip() // belum selesai
    {
        if (_isBeastEquip)
        {
            _damageMeleeExtra = 16;
            _damageMelee += _damageMeleeExtra;
            Save(PlayerPrefsKey.DAMAGE_MELEE_EXTRA, _damageMeleeExtra);
        }

        if (_isDisorderEquip)
        {
            _damageReductionExtra = 5;
            DamageReduction += _damageReductionExtra;
            Save(PlayerPrefsKey.DAMAGE_REDUCTION_EXTRA, _damageReductionExtra);
        }

        if (_isIlusionEquip)
        {
            _damageMagicExtra = 15;
            DamageMagic += _damageMagicExtra;
            Save(PlayerPrefsKey.DAMAGE_MAGIC_EXTRA, _damageMagicExtra);
        }

        if (_isTruthEquip)
        {
            _manaRegenExtra = 5;
            ManaRegen += _manaRegenExtra;
            Save(PlayerPrefsKey.MANA_REGEN_EXTRA, _manaRegenExtra);
        }

        if (_isHarmonyEquip)
        {
            _healthPointExtra = 20;
            _healthPoint += _healthPointExtra;
            Save(PlayerPrefsKey.HEALTHPOINT_EXTRA, _healthPointExtra);
        }

        if (_isSpiritRuneEquip)
        {
            _manaPointExtra = 20;
            ManaPoint += _manaPointExtra;
            Save(PlayerPrefsKey.MANAPOINT_EXTRA, _manaPointExtra);
        }


        if(NotEquipAll())
        {
            Save(PlayerPrefsKey.HEALTHPOINT_EXTRA,      0);
            Save(PlayerPrefsKey.MANAPOINT_EXTRA,        0);
            Save(PlayerPrefsKey.DAMAGE_MELEE_EXTRA,     0);
            Save(PlayerPrefsKey.DAMAGE_MAGIC_EXTRA,     0);
            Save(PlayerPrefsKey.MANA_REGEN_EXTRA,       0);
            Save(PlayerPrefsKey.DAMAGE_REDUCTION_EXTRA, 0);
        }
    }

    private bool NotEquipAll() =>   !_isBeastEquip &&
                                    !_isDisorderEquip && 
                                    !_isHarmonyEquip && 
                                    !_isIlusionEquip && 
                                    !_isSpiritRuneEquip && 
                                    !_isTruthEquip;

    public static void SetValueOnCreateNewGame()
    {
        Save(PlayerPrefsKey.HEALTHPOINT, DEFAULT_HEALTHPOINT);
        Save(PlayerPrefsKey.MANAPOINT, DEFAULT_MANAPOINT);
        Save(PlayerPrefsKey.COIN, 0);

        Save(PlayerPrefsKey.LAST_SCENE,      "map_1");
        Save(PlayerPrefsKey.LAST_CHECKPOINT, "map_1_point_1");

        Save(PlayerPrefsKey.ROCCA_TALK_SESSION,  1);
        Save(PlayerPrefsKey.GERRIN_TALK_SESSION, 1);
        Save(PlayerPrefsKey.GWYNN_TALK_SESSION,  1);

        Save(PlayerPrefsKey.UNLOCKED_MAP,        0);

        Save(PlayerPrefsKey.WATER_SPEAR,             false);
        Save(PlayerPrefsKey.WATER_SPEAR_EQUIP,       false);
        Save(PlayerPrefsKey.INVINCIBLE_SHIELD,       false);
        Save(PlayerPrefsKey.INVINCIBLE_SHIELD_EQUIP, false);

    }
}
