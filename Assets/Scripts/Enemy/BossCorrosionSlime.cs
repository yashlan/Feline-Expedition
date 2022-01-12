using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCorrosionSlime : Enemy
{
    [Header("Bullet")]
    public Transform bulletPoint;
    public GameObject bullet;

    EnemyBulletMelee bulletMelee;
    GameObject bulletTemp;
    GameObject bulletClone;

    void Start()
    {
        SetNewStats(enemyType);
        SetFirstPosition();

        bulletMelee = bullet.GetComponent<EnemyBulletMelee>();

        bulletMelee.midBoss = this;
        bulletMelee._damage = DamageAir;

        bulletTemp = Instantiate(bullet);
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
                    if (!hit)
                    {
                        var spawnLength = FindObjectsOfType<EnemyGreenSlime>().Length;

                        Shoot(spawnLength == 0 && DistanceToPlayer() >= 25 && DistanceToPlayer() <= 30);

                        if (DistanceToPlayer() <= AttackRadius)
                            MoveToTarget();

                        if (DistanceToPlayer() > AttackRadius)
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

    /// <summary>
    /// event animasi
    /// </summary>
    public void ShootEvent()
    {
        bulletClone = Instantiate(bulletTemp, bulletPoint.position, Quaternion.identity, null);
        bulletClone.SetActive(true);
    }
}
