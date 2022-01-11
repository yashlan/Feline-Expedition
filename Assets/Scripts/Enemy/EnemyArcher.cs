using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Enemy
{
    [Header("Arrow")]
    public Transform arrowPoint;
    public GameObject arrow;

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
        Instantiate(arrow, arrowPoint.position, Quaternion.identity, null);
    }
}
