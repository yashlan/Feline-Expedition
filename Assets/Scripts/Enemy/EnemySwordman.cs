

public class EnemySwordman : Enemy
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

        if (GameManager.GameState == GameState.Playing)
        {
            GroundCheck();
            CheckHitPlayer((hit) => {

                if (hit)
                {
                    Attack();
                }
                else
                {
                    if (DistanceToPlayer() <= AttackRadius)
                        MoveToTarget();
                    else
                    {
                        StopMove();
                    }
                }
            });
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
