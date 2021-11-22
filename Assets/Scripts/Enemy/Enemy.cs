using UnityEngine;
public enum EnemyType
{
    Easy, Medium, Hard, Expert, BossBattle
}

public class Enemy : MonoBehaviour
{
    [Header("Melee")]
    public EnemyMelee EnemyMelee;

    public float AttackRadius;

    public bool FacingRight;

    public Animator Anim;
    public Rigidbody2D Rigidbody;

    [Header("Ground Raycast")]
    public Transform GroundCheckPoint;
    public float GroundCastDistance;
    public LayerMask GroundMask;

    [Header("Stat Enemy")]
    public EnemyType EnemyType;
    public int HealthPoint;
    public int Damage;
    public float Speed;
    public float CoolDownAttack;

    PlayerController _target => PlayerController.Instance;
    float distance;
    float delayAttack;
    RaycastHit2D hitGround;
    float horizontal;

    public void SetNewStats(
        EnemyType EnemyType, 
        int HealthPoint, 
        int Damage, 
        float Speed,
        float CoolDownAttack)
    {
        this.EnemyType = EnemyType;
        this.HealthPoint = HealthPoint;
        this.Damage = Damage;
        this.Speed = Speed;
        this.CoolDownAttack = CoolDownAttack;
    }

    public void Movement()
    {
        if (_target == null) return;

        Rigidbody.velocity = new Vector2(
            (_target.transform.position.x > transform.position.x) && hitGround ? Speed : -Speed, 0);
    }

    public void HandleFacing()
    {
        horizontal = Speed > 0 ? 1 : -1;

        if (horizontal > 0 && !FacingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void GroundCheck()
    {
        hitGround = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, GroundCastDistance, GroundMask);

        if (!hitGround) 
            Speed = FacingRight ? -Speed : Speed;
    }

    public void SetAttackPoint()
    {
        if (_target == null) return;

        if (hitGround)
        {
            distance = Vector2.Distance(transform.position, _target.transform.position);

            if (distance <= AttackRadius && _target.IsGrounded)
            {
                transform.LookAt(_target.transform);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (delayAttack < Time.time && distance <= 7f)
            {
                print("attacking");
                Anim.SetBool("Attack", true);
                delayAttack = Time.time + CoolDownAttack;
            }

            Rigidbody.constraints = Anim.GetBool("Attack") ?
                RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        }
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
    #endregion
}
