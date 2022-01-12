using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    [Header("Arrow")]
    public Transform arrowPoint;
    public GameObject arrow;

    GameObject arrowTemp;
    GameObject arrowClone;

    EnemyBulletMelee bulletMelee;

    void Start()
    {
        SetNewStats(enemyType);
        SetFirstPosition();

        bulletMelee = arrow.GetComponent<EnemyBulletMelee>();

        bulletMelee.archer = this;
        bulletMelee._damage = DamageAir;

        arrowTemp = Instantiate(arrow);
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
                    Shoot(hit);
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
        arrowClone = Instantiate(arrowTemp, arrowPoint.position, Quaternion.identity, null);
        arrowClone.SetActive(true);
    }
}
