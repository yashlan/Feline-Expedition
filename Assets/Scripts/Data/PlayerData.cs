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

    [Header("UnlockedMap")]
    [SerializeField]
    private bool _isMapUnlocked;

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
    private bool _isSpiritRuneWasPurchased;

    [Header("Unlocked Rune")]
    [SerializeField]
    private bool _isWaterSpearWasUnlocked;
    [SerializeField]
    private bool _isInvincibleShieldWasUnlocked;

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
    private int _npcGwynnTalkSession = 1;
    [SerializeField]
    private int _npcRoccaTalkSession = 1;

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

    public static bool IsWaterSpearWasUnlocked 
    { 
        get => Instance._isWaterSpearWasUnlocked; 
        set => Instance._isWaterSpearWasUnlocked = value; 
    }

    public static bool IsInvincibleShieldWasUnlocked 
    { 
        get => Instance._isInvincibleShieldWasUnlocked; 
        set => Instance._isInvincibleShieldWasUnlocked = value; 
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

    #endregion

    public static bool IsInvincibleShieldUsed() => IsInvincibleShieldWasUnlocked && IsInvincibleShieldEquip;
    public static bool IsWaterSpearUsed() => IsWaterSpearWasUnlocked && IsWaterSpearEquip;

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

    #region LOAD

    private void Load()
    {
        _isMapUnlocked = BoolValueOf(PlayerPrefs.GetInt(PlayerPrefsKey.UNLOCKED_MAP, 0));

        #region NPC
        _npcGerrinTalkSession = PlayerPrefs.GetInt(PlayerPrefsKey.GERRIN_TALK_SESSION, _npcGerrinTalkSession);
        _npcGwynnTalkSession  = PlayerPrefs.GetInt(PlayerPrefsKey.GWYNN_TALK_SESSION,  _npcGwynnTalkSession);
        _npcRoccaTalkSession  = PlayerPrefs.GetInt(PlayerPrefsKey.ROCCA_TALK_SESSION,  _npcRoccaTalkSession);
        #endregion

        #region SHOP
        _coins = PlayerPrefs.GetInt(PlayerPrefsKey.COIN,                             _coins);
        _totalRuneSlotUsed = PlayerPrefs.GetInt(PlayerPrefsKey.TOTAL_RUNE_SLOT_USED, _totalRuneSlotUsed);
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
        if (_isBeastEquip /*&& _totalSlot > 0*/)
        {
            _damageMeleeExtra = 8;
            _damageMelee += _damageMeleeExtra;
            Save(PlayerPrefsKey.DAMAGE_MELEE_EXTRA, _damageMeleeExtra);
        }

        if (_isDisorderEquip)
        {
            _damageReductionExtra = 2;
            DamageReduction += _damageReductionExtra;
            Save(PlayerPrefsKey.DAMAGE_REDUCTION_EXTRA, _damageReductionExtra);
        }

        if (_isIlusionEquip)
        {
            _damageMagicExtra = 7;
            DamageMagic += _damageMagicExtra;
            Save(PlayerPrefsKey.DAMAGE_MAGIC_EXTRA, _damageMagicExtra);
        }

        if (_isTruthEquip)
        {
            //+2 mana regen
            _manaRegenExtra = 2;
            ManaRegen += _manaRegenExtra;
            Save(PlayerPrefsKey.MANA_REGEN_EXTRA, _manaRegenExtra);
        }

        if (_isHarmonyEquip)
        {
            _healthPointExtra = 10;
            _healthPoint += _healthPointExtra;
            Save(PlayerPrefsKey.HEALTHPOINT_EXTRA, _healthPointExtra);
        }

        if (_isSpiritRuneEquip)
        {
            _manaPointExtra = 10;
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
}
