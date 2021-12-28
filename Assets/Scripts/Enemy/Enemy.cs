using System;
using System.Collections;
using UnityEngine;
public enum EnemyType
{
    GreenSlime, 
    Swordman, 
    Shieldman,
    Archer,
    CorrotionSlime,
}

public class Enemy : MonoBehaviour
{
    [Header("Type")]
    public EnemyType enemyType;

    [Header("Melee")]
    public EnemyMelee EnemyMelee;

    [Header("Facing")]
    public bool FacingRight;

    [Header("KnockBack")]
    public bool isKnock;

    [Header("Stat Enemy")]
    public int HealthPoint;
    public int Damage;
    public int DamageAir;
    public int DamageReduction;
    public float Speed;
    public float CoolDownAttack;
    public int CoinReward;
    public bool IsDead;

    [Header("Attack when Cast")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask PlayerMask;

    [Header("find target")]
    public float AttackRadius;

    [Header("Ground Raycast")]
    public Transform GroundCheckPoint;
    public float GroundCastDistance;
    public LayerMask GroundMask;

    [Header("Block/Defend Armor")]
    public bool IsBlocking;

    [HideInInspector] public Animator Anim;
    [HideInInspector] public Rigidbody2D Rigidbody;

    float delayAttack;
    float delayFlip;
    RaycastHit2D hitGround;
    float horizontal;
    Vector3 firstPos;
    BoxCollider2D _boxCollider;
    float shootDelay;
    float blockDelay;

    public PlayerController _target => PlayerController.Instance;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Setup(
        int healthPoint, 
        int damage, 
        int damageAir, 
        int damageReduction, 
        float speed, 
        float coolDownAttack,
        int coinReward)
    {
        HealthPoint = healthPoint;
        Damage = damage;
        DamageAir = damageAir;
        DamageReduction = damageReduction;
        Speed = speed;
        CoolDownAttack = coolDownAttack;
        CoinReward = coinReward;
    }

    public void SetNewStats(EnemyType enemyType)
    {
        if (enemyType == EnemyType.GreenSlime)     Setup(25,   5, 0,  0,  7, 0.7f, 5);
        if (enemyType == EnemyType.Swordman)       Setup(40,   5, 0,  5,  7, 1f,  15);
        if (enemyType == EnemyType.Shieldman)      Setup(40,   5, 0,  5,  7, 3f,  15);
        if (enemyType == EnemyType.Archer)         Setup(40,   0, 3,  0,  0, 2f,  5);
        if (enemyType == EnemyType.CorrotionSlime) Setup(300, 10, 15, 10, 4, 3f,  200);
    }

    public float DistanceToPlayer() =>
        Vector2.Distance(transform.position, _target.transform.position);

    public float DistanceY() => (_target.transform.position.y - transform.position.y);

    public void SetFirstPosition()
    {
        firstPos = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = firstPos;
    }

    public void Block(bool canBlock)
    {
        if(canBlock)
        {
            Anim.SetTrigger("Block");
        }
    }

    public void Shoot(bool canShot)
    {
        if(canShot && Time.time > shootDelay)
        {
            Anim.SetTrigger("Shoot");
            shootDelay = Time.time + CoolDownAttack;
        }
    }

    public void Attack()
    {
        if (!isKnock)
        {
            if (hitGround)
            {
                if (Time.time > delayAttack)
                {
                    Anim.SetTrigger("Attack");
                    delayAttack = Time.time + CoolDownAttack;
                }
            }
        }
    }

    public void CheckHitPlayer(Action<bool> OnHit)
    {
        var hitPlayer = Physics2D.Raycast(
            attackPoint.position,
            (FacingRight ? Vector2.right : Vector2.left),
            attackRange,
            PlayerMask);

        OnHit(hitPlayer);
    }

    public void MoveToTarget()
    {
        if (enemyType != EnemyType.GreenSlime)
            Anim.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));

        Rigidbody.velocity = new Vector2(Speed * horizontal, 0);
        transform.LookAt(_target.transform);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveToFirstPosition()
    {
        firstPos.y = transform.position.y;
        Rigidbody.velocity = new Vector2(transform.position.x < firstPos.x ? Speed : -Speed, 0);
        transform.LookAt(firstPos);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopMove()
    {
        Rigidbody.velocity = Vector2.zero;
        if (enemyType != EnemyType.GreenSlime)
            Anim.SetFloat("Speed", 0);
    }

    public void HandleFacing()
    {
        if (enemyType == EnemyType.Archer)
        {
            Flip();
            return;
        }

        if (DistanceToPlayer() <= AttackRadius)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if(Time.time > delayFlip)
        {
            var scale = transform.localScale;

            scale.x = Mathf.Abs(scale.x);

            if (_target.transform.position.x < transform.position.x) scale.x *= -1;

            transform.localScale = scale;

            FacingRight = scale.x > 0;

            horizontal = scale.x > 0 ? 1 : -1;

            delayFlip = Time.time + 0.5f;
        }
    }

    public void GroundCheck()
    {
        hitGround = Physics2D.Raycast(
            GroundCheckPoint.position, 
            Vector2.down, 
            GroundCastDistance, 
            GroundMask);

        if (!hitGround) 
            Speed = FacingRight ? -Speed : Speed;
    }

    public void KnockBack(float force)
    {
        StartCoroutine(IKnockBack(force));
    }

    IEnumerator IKnockBack(float force)
    {
        isKnock = true;
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);

        Rigidbody.AddForce(
            (_target.transform.position.x < transform.position.x ? 
            Vector2.right : Vector2.left) * force,
            ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isKnock = false;
    }

    public void Dead()
    {
        _target.AddCoin(CoinReward);
        PlayerCoinsUI.UpdateUI();

        IsDead = true;

        if (enemyType == EnemyType.GreenSlime)
        {
            Destroy(gameObject);
            return;
        }

        Rigidbody.bodyType = RigidbodyType2D.Static;
        Anim.SetTrigger("Dead");
        gameObject.layer = LayerMask.NameToLayer("Default");
        _boxCollider.enabled = false;
    }


    #region DEBUG
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(GroundCheckPoint.position, GroundCheckPoint.position + (Vector3.down * GroundCastDistance));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(attackPoint.position, attackPoint.position + ((!FacingRight ? Vector3.left : Vector3.right) * attackRange));
    }
    #endregion

    #region EVENT ANIMATION
    public void AttackEvent()
    {
        EnemyMelee.StartMelee();
    }

    public void DisableAttackEvent()
    {
        Anim.SetBool("Attack", false);
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }
    #endregion
}
