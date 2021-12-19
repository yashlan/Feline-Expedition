
public class PlayerPrefsKey : SingletonDontDestroy<PlayerPrefsKey>
{
    #region SOUND 
    public const string VOLUME_BGM = "VOLUME_BGM";
    public const string VOLUME_SFX = "VOLUME_SFX";
    #endregion

    #region VIDEO SETTING
    public const string DISPLAY_MODE_FULLSCREEN = "DISPLAY_MODE_FULLSCREEN";
    public const string GRAPHIC_QUALITY         = "GRAPHIC_QUALITY";
    public const string DISPLAY_PLAYER_UI       = "DISPLAY_PLAYER_UI";
    #endregion

    #region INPUT KEYCODE
    public const string MOVE_LEFT    = "MOVE_LEFT";
    public const string MOVE_RIGHT   = "MOVE_RIGHT";
    public const string JUMP         = "JUMP";
    public const string DASH         = "DASH";
    public const string ATTACK_MELEE = "ATTACK_MELEE";
    public const string ATTACK_THROW = "ATTACK_THROW";
    public const string RECHARGE     = "RECHARGE";
    public const string INTERACTION  = "INTERACTION";
    public const string OPEN_MAP     = "OPEN_MAP";
    #endregion

    #region PLAYER STATS DATA
    //public const string NOT_FIRST_PLAY   = "NOT_FIRST_PLAY";
    public const string HEALTHPOINT      = "HEALTHPOINT";
    public const string MANAPOINT        = "MANAPOINT";
    public const string DAMAGE_MELEE     = "DAMAGE_MELEE";
    public const string DAMAGE_MAGIC     = "DAMAGE_MAGIC";
    public const string DAMAGE_REDUCTION = "DAMAGE_REDUCTION";
    public const string MANA_REGEN       = "MANA_REGEN";
    #endregion

    #region PLAYER STATS DATA EXTRA
    public const string HEALTHPOINT_EXTRA      = "HEALTHPOINT_EXTRA";
    public const string MANAPOINT_EXTRA        = "MANAPOINT_EXTRA";
    public const string DAMAGE_REDUCTION_EXTRA = "DAMAGE_REDUCTION_EXTRA";
    public const string DAMAGE_MELEE_EXTRA     = "DAMAGE_MELEE_EXTRA";
    public const string DAMAGE_MAGIC_EXTRA     = "DAMAGE_MAGIC_EXTRA";
    public const string MANA_REGEN_EXTRA       = "MANA_REGEN_EXTRA";
    #endregion

    #region CHECKPOINT
    public const string LAST_SCENE      = "LAST_SCENE";
    public const string LAST_CHECKPOINT = "LAST_CHECKPOINT";
    #endregion

    #region SHOP
    public const string COIN                 = "COIN";
    public const string TOTAL_RUNE_SLOT_USED = "TOTAL_RUNE_SLOT_USED";
    #endregion
}
