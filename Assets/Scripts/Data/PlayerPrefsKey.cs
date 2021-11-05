
public class PlayerPrefsKey : SingletonDontDestroy<PlayerPrefsKey>
{
    #region INPUT KEYCODE
    public const string MOVE_LEFT = "MOVE_LEFT";
    public const string MOVE_RIGHT = "MOVE_RIGHT";
    public const string JUMP = "JUMP";
    public const string DASH = "DASH";
    public const string ATTACK_MELEE = "ATTACK_MELEE";
    public const string ATTACK_THROW = "ATTACK_THROW";
    public const string RECHARGE = "RECHARGE";
    #endregion


    #region PLAYER STATS DATA
    public const string HEALTHPOINT = "HEALTHPOINT";
    public const string DAMAGE_MELEE = "DAMAGE_MELEE";
    public const string MOVE_SPEED = "MOVE_SPEED";
    public const string CAN_DOUBLE_JUMP = "CAN_DOUBLE_JUMP";
    public const string CAN_DASHING = "CAN_DASHING";
    #endregion

}
