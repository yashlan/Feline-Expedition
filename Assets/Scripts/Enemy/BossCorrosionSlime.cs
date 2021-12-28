using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCorrosionSlime : Enemy
{
    [Header("Bullet")]
    public Transform bulletPoint;
    public GameObject bullet;

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

                if (hit && DistanceToPlayer() <= AttackRadius)
                {
                    Attack();
                }
                else
                {
                    Shoot(!hit && DistanceToPlayer() <= AttackRadius);

                    if(DistanceToPlayer() <= AttackRadius)
                        MoveToTarget();
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

    /// <summary>
    /// event animasi
    /// </summary>
    public void ShootEvent()
    {
        Instantiate(bullet, bulletPoint.position, Quaternion.identity, null);
    }
}
