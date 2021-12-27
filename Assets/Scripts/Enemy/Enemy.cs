using System;
using System.Collections;
using UnityEngine;
public enum EnemyType
{
    GreenSlime, 
    Swordman, 
    Shieldman,
    Archer,
}

public class Enemy : MonoBehaviour
{
    [Header("Type")]
    public EnemyType enemyType;

    [Header("Melee")]
    public EnemyMelee EnemyMelee;
    public float AttackRadius;

    [Header("Facing")]
    public bool FacingRight;

    [Header("KnockBack")]
    public bool isKnock;

    [Header("Stat Enemy")]
    public int HealthPoint;
    public int Damage;
    public int DamageReduction;
    public float Speed;
    public float CoolDownAttack;
    public int CoinReward;
    public bool IsDead;



    [Header("Ground Raycast")]
    public Transform GroundCheckPoint;
    public float GroundCastDistance;
    public LayerMask GroundMask;

    [HideInInspector] public Animator Anim;
    [HideInInspector] public Rigidbody2D Rigidbody;

    float distance;
    float delayAttack;
    RaycastHit2D hitGround;
    float horizontal;
    Vector3 firstPos;
    BoxCollider2D _boxCollider;
    float shootDelay;

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
        int damageReduction, 
        float speed, 
        float coolDownAttack,
        int coinReward)
    {
        HealthPoint = healthPoint;
        Damage = damage;
        DamageReduction = damageReduction;
        Speed = speed;
        CoolDownAttack = coolDownAttack;
        CoinReward = coinReward;
    }

    public void SetFirstPosition()
    {
        firstPos = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = firstPos;
    }

    private void Block()
    {
        if(enemyType == EnemyType.Shieldman)
        {
            Anim.SetTrigger("Block");
        }
    }

    public void ShootArrow()
    {
        if(enemyType == EnemyType.Archer)
        {
            distance = Vector2.Distance(transform.position, _target.transform.position);

            var distanceY = _target.transform.position.y - transform.position.y;

            if (distance <= AttackRadius && Time.time > shootDelay && _target.IsGrounded && distanceY < 2)
            {
                Anim.SetTrigger("Shoot");
                shootDelay = Time.time + CoolDownAttack;
            }
        }
    }

    public void SetNewStats(EnemyType enemyType)
    {
        if(enemyType == EnemyType.GreenSlime) Setup(25, 5,  0, 7, 0.7f,  5);        
        if(enemyType == EnemyType.Swordman)   Setup(40, 5,  5, 7,   1f, 15);
        if(enemyType == EnemyType.Shieldman)  Setup(40, 10, 5, 7,   2f, 15);
        if(enemyType == EnemyType.Archer)     Setup(40, 3,  0, 0,   3f,  5);
    }

    private void MoveToTarget()
    {
        Rigidbody.velocity = new Vector2(Speed * horizontal, 0);
        transform.LookAt(_target.transform);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void MoveToFirstPosition()
    {
        Rigidbody.velocity = new Vector2(transform.position.x < firstPos.x ? Speed : -Speed, 0);
        transform.LookAt(firstPos);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void HandleFacing()
    {
        Flip();
    }

    private void Flip()
    {
        var scale = transform.localScale;

        scale.x = Mathf.Abs(scale.x);

        if (_target.transform.position.x < transform.position.x) scale.x *= -1;

        transform.localScale = scale;

        FacingRight = scale.x > 0;

        horizontal = scale.x > 0 ? 1 : -1;
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

    public void SetAttackState()
    {
        if (!isKnock)
        {
            if (hitGround)
            {
                distance = Vector2.Distance(transform.position, _target.transform.position);

                var distanceY = _target.transform.position.y - transform.position.y;

                if (distance <= AttackRadius && _target.IsGrounded && distanceY < 2)
                {
                    MoveToTarget();

                    //if(distance <= 4)
                    //    Block();
                }
                else
                    MoveToFirstPosition();

                if (Time.time > delayAttack && distance < 6f)
                {
                    Anim.SetTrigger("Attack");
                    delayAttack = Time.time + CoolDownAttack;
                }
            }
        }
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

    public void MovementSkeleton()
    {
        Anim.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));
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
