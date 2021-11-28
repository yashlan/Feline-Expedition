using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    [Header("Ability")]
    [SerializeField]
    private bool _canAttack;

    // Start is called before the first frame update
    void Start()
    {
        SetNewStats(EnemyType.Easy, 1000, 5, 0f, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameState == GameState.Playing)
        {
            GroundCheck();
            Movement();
            SetAttackPoint();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.GameState == GameState.Playing)
        {
            HandleFacing();
        }
    }
}
