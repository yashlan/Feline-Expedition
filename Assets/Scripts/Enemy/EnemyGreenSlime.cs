
public class EnemyGreenSlime : Enemy
{
    void Start()
    {
        SetNewStats(enemyType);
        SetFirstPosition();
    }

    void Update()
    {
        if (_target.IsDead)
            return;

        if(GameManager.GameState == GameState.Playing)
        {
            GroundCheck();
            SetAttackState();
        }
    }

    void FixedUpdate()
    {
        if (_target.IsDead)
            return;

        if (GameManager.GameState == GameState.Playing)
        {
            HandleFacing();
        }
    }
}
