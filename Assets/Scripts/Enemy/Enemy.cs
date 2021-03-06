using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType
{
    GreenSlime, 
    Swordman, 
    Shieldman,
    Archer,
    CorrosionSlime,
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

    [Header("Animation State")]
    public bool IsBlocking;
    public bool IsShooting;
    public bool IsAttacking;

    [HideInInspector] public Animator Anim;
    [HideInInspector] public Rigidbody2D Rigidbody;

    float delayAttack;
    float delayFlip;
    float delayShoot;

    RaycastHit2D hitGround;
    float horizontal;
    Vector3 firstPos;


    public PlayerController _target => PlayerController.Instance;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        Rigidbody.gravityScale = 300;
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

        delayAttack = delayShoot = Time.time + CoolDownAttack;
    }

    public void SetNewStats(EnemyType enemyType)
    {
        if (enemyType == EnemyType.GreenSlime)     Setup(25,  3, 0, 0, 7, 0.7f, 5);
        if (enemyType == EnemyType.Swordman)       Setup(40,  3, 0, 5, 7, 1f,  15);
        if (enemyType == EnemyType.Shieldman)      Setup(40,  3, 0, 5, 7, 2f,  15);
        if (enemyType == EnemyType.Archer)         Setup(40,  0, 3, 0, 0, 2f,   5);
        if (enemyType == EnemyType.CorrosionSlime) Setup(300, 5, 3, 2, 4, 3f, 200);
    }

    public float DistanceToPlayer() =>
        Vector2.Distance(transform.position, _target.transform.position);

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
        if (isKnock || !hitGround)
            return;

        if (canBlock)
        {
            Anim.SetTrigger("Block");
        }
    }

    public void Shoot(bool canShot)
    {
        if (isKnock || !hitGround)
            return;

        if (canShot && Time.time > delayShoot && !IsAttacking)
        {
            Anim.SetTrigger("Shoot");
            delayShoot = Time.time + CoolDownAttack;
        }
    }

    public void Attack()
    {
        if (isKnock || !hitGround)
            return;

        if (Time.time > delayAttack && !IsShooting)
        {
            Anim.SetTrigger("Attack");
            delayAttack = Time.time + CoolDownAttack;
        }
    }

    public void CheckHitPlayer(Action<bool> OnHit)
    {
        var hitPlayer = Physics2D.Raycast(
            attackPoint.position,
            FacingRight ? Vector2.right : Vector2.left,
            attackRange,
            PlayerMask);

        OnHit(hitPlayer);
    }

    public void MoveToTarget()
    {
        if (isKnock || !hitGround)
            return;

        if (enemyType != EnemyType.GreenSlime)
            Anim.SetFloat("Speed", Mathf.Abs(Rigidbody.velocity.x));

        Rigidbody.velocity = new Vector2(Speed * horizontal, 0);
        transform.LookAt(_target.transform);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void MoveToFirstPosition()
    {
        if (isKnock)
            return;

        firstPos.y = transform.position.y;
        Rigidbody.velocity = new Vector2(transform.position.x < firstPos.x ? Speed : -Speed, 0);
        transform.LookAt(firstPos);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopMove()
    {
        if (isKnock)
            return;

        Rigidbody.velocity = Vector2.zero;

        if (enemyType != EnemyType.GreenSlime || enemyType != EnemyType.Archer)
        {
            Anim.SetFloat("Speed", 0);
        }
    }

    public void HandleFacing()
    {
        if (isKnock)
            return;

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
            GroundCheckPoint.position + (Vector3.down * GroundCastDistance), 
            Vector2.down, 
            GroundCastDistance, 
            GroundMask);

        if (!hitGround)
            StopMove();
    }

    public void KnockBack(float force)
    {
        if(enemyType != EnemyType.CorrosionSlime)
        {
            StartCoroutine(IKnockBack(force));
        }
    }

    int knockCount = 0;

    IEnumerator IKnockBack(float force)
    {
        knockCount++;

        if(knockCount > 1)
        {
            knockCount = 0;
            isKnock = true;
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);

            Rigidbody.AddForce(
                (_target.transform.position.x < transform.position.x ?
                Vector2.right : Vector2.left) * force,
                ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            isKnock = false;
        }
    }

    public void Dead()
    {
        _target.AddCoin(CoinReward);
        PlayerCoinsUI.UpdateUI();

        IsDead = true;

        if (enemyType == EnemyType.GreenSlime)
        {
            AudioManager.PlaySfx(AudioManager.Instance.EnemySlimeDeadClip);
            Destroy(gameObject);
            return;
        }

        if (GetComponent<BoxCollider2D>() != null)
            GetComponent<BoxCollider2D>().enabled = false;

        if (GetComponent<CircleCollider2D>() != null)
            GetComponent<CircleCollider2D>().enabled = false;

        gameObject.layer = LayerMask.NameToLayer("Dead");
        Rigidbody.bodyType = RigidbodyType2D.Static;
        Anim.SetTrigger("Dead");

        if (enemyType == EnemyType.CorrosionSlime)
            AudioManager.PlaySfx(AudioManager.Instance.MidBossDeadClip);

        if (enemyType == EnemyType.Shieldman)
            AudioManager.PlaySfx(AudioManager.Instance.EnemyShieldManDeadClip);

        if (enemyType == EnemyType.Swordman)
            AudioManager.PlaySfx(AudioManager.Instance.EnemySwordManDeadClip);

        if (enemyType == EnemyType.Archer)
            AudioManager.PlaySfx(AudioManager.Instance.EnemyArcherDeadClip);
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
        if(enemyType == EnemyType.CorrosionSlime)
        {
            if(FindObjectsOfType<EnemyGreenSlime>().Length > 0)
            {
                foreach (var enemy in FindObjectsOfType<EnemyGreenSlime>())
                {
                    enemy.Dead();
                }
            }
            StartCoroutine(IOnBossDead());
            return;
        }

        Destroy(gameObject);
    }

    IEnumerator IOnBossDead()
    {
        yield return new WaitUntil(() => _target.IsIdle());

        PlayerData.Delete(PlayerPrefsKey.LAST_SCENE);

        PlayerController.Instance.Rigidbody.velocity = Vector2.zero;
        PlayerController.FreezePosition();
        PlayerController.Instance.enabled = false;
        yield return new WaitForSeconds(2f);
        PanelSlideUIController.Instance.FadeIn(() => OnFadeIn(), 0f);
        yield return new WaitForSeconds(10f);
        OptionsManager.ShowMouseCursor();
        AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[0]);
        SceneManager.LoadScene("home");
    }

    void OnFadeIn()
    {
        GameManager.ShowGameOverText("Thanks For Playing");
    }
    #endregion
}
