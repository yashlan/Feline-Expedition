public class EnemyShieldman : Enemy
{
     void Start()
    {
        SetNewStats(enemyType);
        SetFirstPosition();
    }

    void Update()
    {
        if (_target.IsDead || IsDead)
            return;

        if(GameManager.GameState == GameState.Playing)
        {
            GroundCheck();
            SetAttackState();
            MovementSkeleton();
        }
    }

    void FixedUpdate()
    {
        if (_target.IsDead || IsDead)
            return;

        if (GameManager.GameState == GameState.Playing)
        {
            HandleFacing();
        }
    }
}
