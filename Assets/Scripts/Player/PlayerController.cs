using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Melee")]
    [SerializeField]
    private PlayerMelee _meleeAttack1;
    [SerializeField]
    private PlayerMelee _meleeAttack2;
    [SerializeField]
    private PlayerMelee _meleeSpearAttack;

    [Header("Fireball")]
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private Transform _throwPoint;

    [Header("Player Stats")]
    [SerializeField]
    private int _healthPoint;
    [SerializeField]
    private int _manaPoint;
    [SerializeField]
    private int _damageReduction;
    [SerializeField]
    private int _damageMelee;
    [SerializeField]
    private int _damageMagic;
    [SerializeField]
    private int _manaRegen;
    [SerializeField]
    private float _coolDownAttack = 0.25f;
    [SerializeField]
    private float _coolDownSpearAttack = 0.25f;
    [SerializeField]
    private float _coolDownJumpAttack = 0.25f;
    [SerializeField]
    private float _coolDownDash = 0.25f;
    [SerializeField]
    private float _coolDownThrow = 0.25f;
    [SerializeField]
    private float _moveSpeed = 25f;
    [SerializeField]
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _jumpAmount = 1;
    [SerializeField]
    private float _jumpForce = 40f;

    [Header("Dash & Shadow Effect")]
    [SerializeField]
    private bool _canDashing = false;
    [SerializeField]
    private int _dashAmountOnAir = 1;
    [SerializeField]
    private float _dashDistance = 2f;
    [SerializeField]
    private float _delayShadow = 0.01f;
    [SerializeField]
    private float _destroyTimeShadow = 0.1f;
    [SerializeField]
    private GameObject _dashFX;
    [SerializeField]
    private GameObject _shadow;

    [Header("Wall Jump")]
    [SerializeField]
    private bool _isTouchingWall;
    [SerializeField]
    private bool _isWallSliding;
    [SerializeField]
    private float _wallJumpForce;
    [SerializeField]
    private float _wallSlideSpeed = 1f;
    [SerializeField]
    private Transform _wallCheckPoint;
    [SerializeField]
    private Vector2 _wallCheckSize;
    [SerializeField]
    private LayerMask _wallLayerMask;

    [Header("Ground Raycast")]
    [SerializeField]
    private Vector3 _groundOffset;
    [SerializeField]
    private float _groundRaycastDistance;
    [SerializeField]
    private LayerMask _groundLayerMask;

    [Header("Check Facing")]
    [SerializeField]
    private bool _facingRight = true;

    [Header("Player Anim State")]
    [SerializeField]
    private bool _isThrowing;
    [SerializeField]
    private bool _isDefend;
    [SerializeField]
    private bool _isCharging;
    [SerializeField]
    private bool _isDashing;
    [SerializeField]
    private bool _isAttacking;
    [SerializeField]
    private bool _isJumpAttack;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isMoving;
    [SerializeField]
    private bool _isJumping;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Animator _anim;
    private GameObject _dashFXClone;

    private float _horizontal;

    private float _delayAttack;
    private float _delayJumpAttack;
    private float _delayThrow;
    private float _delayDash;
    private float _delayLastPos;
    private float _delaycanRecharge;
    private float _delaycanShowShield;

    private float _delta;

    private Vector3 _lastPos;

    public int HealthPoint { get => _healthPoint; set => _healthPoint = value; }
    public int DamageMelee => _damageMelee;
    public int DamageMagic => _damageMagic;
    public float CoolDownAttack => _coolDownAttack;
    public float CoolDownSpearAttack => _coolDownSpearAttack;

    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool IsCharging => _isCharging;
    public bool IsDefend => _isDefend;
    public bool FacingRight => _facingRight;

    public Animator Anim => _anim;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //load stats data
        LoadPlayerData(
            PlayerData.HealthPoint,
            PlayerData.ManaPoint,
            PlayerData.DamageReduction,
            PlayerData.DamageMelee,
            PlayerData.DamageMagic,
            PlayerData.ManaRegen);
    }

    void Update()
    {
        InputPlayer();
        CheckIsTouchWall();
        CheckGround();

        //buat reset pos, sementara
        if (transform.position.y <= -30f) transform.position = _lastPos;

        if (_isGrounded || _isTouchingWall)
        {
            _dashAmountOnAir = 1;
            _jumpAmount = _canDoubleJump ? 2 : 1;
        }

        //goto menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        WallSlide();
        HandleFacing();
        SaveLastTransform(0.5f);

        if (_isDashing)
            StartDashing();
    }

    #region INPUT

    private void InputPlayer()
    {
        Movement(false);
        Attack(false);
        JumpAttack(false);
        ReChargingMana();
        Jump();
        Dash();

        if (PlayerData.IsInvincibleShieldWasUnlocked)
            ShowInvincibleShield();
        else
            Throw();
    }

    private void Movement(bool _withShadowEffect)
    {
        if (!_isCharging || !_isDefend)
        {
            if (!_isDashing && (!_isWallSliding || !_isTouchingWall))
            {
                if (Input.GetKey(OptionsManager.LeftKey))
                {
                    _horizontal = -1;
                    _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
                }
                else if (Input.GetKey(OptionsManager.RightKey))
                {
                    _horizontal = 1;
                    _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
                }
                else
                {
                    _horizontal = 0;
                    _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
                }

                _anim.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
                _anim.SetFloat("vSpeed", _rb.velocity.y);
                _isMoving = _anim.GetFloat("Speed") != 0f;

                if (_isMoving && _withShadowEffect && _isGrounded)
                {
                    if (_delta > 0)
                        _delta -= Time.deltaTime;
                    else
                    {
                        _delta = _delayShadow;
                        CreateShadowEffect();
                    }
                }
            }
        }
    }

    bool _wasFirstJump = false;
    private void Jump()
    {
        if (Input.GetKeyDown(OptionsManager.JumpKey))
        {
            if (_isGrounded && _jumpAmount > 0)
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.up * _jumpForce;
                _wasFirstJump = true;
                Invoke(nameof(DecreaseJumpAmount), 0.1f);
            }
            else if (!_isGrounded && _jumpAmount == 1 && _wasFirstJump)
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.up * _jumpForce;
                _wasFirstJump = false;
                Invoke(nameof(DecreaseJumpAmount), 0.1f);
            }
            else if ((Input.GetKey(OptionsManager.LeftKey) && _facingRight ||
                !_facingRight && Input.GetKey(OptionsManager.RightKey)) && 
                (_isWallSliding || _isTouchingWall) && _jumpAmount > 0)
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.up * _wallJumpForce;

                _wasFirstJump = true;
                Invoke(nameof(DecreaseJumpAmount), 0.1f);
            }
            else if ((Input.GetKey(OptionsManager.LeftKey) && _facingRight ||
                !_facingRight && Input.GetKey(OptionsManager.RightKey)) && 
                (_isWallSliding || _isTouchingWall) && _jumpAmount == 1 && _wasFirstJump)
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.up * _wallJumpForce;

                _wasFirstJump = false;
                Invoke(nameof(DecreaseJumpAmount), 0.1f);
            }
        }

        _isJumping = !_isGrounded;
    }

    private void DecreaseJumpAmount() => _jumpAmount--;

    private void JumpAttack(bool _withDashEffect)
    {
        if (Input.GetKeyDown(OptionsManager.AttackMeleeKey) && _isJumping
            && Time.time > _delayJumpAttack && !_isCharging)
        {
            _isJumpAttack = true;

            if (_canDashing && _withDashEffect)
                _isDashing = true;

            _delayJumpAttack = Time.time + _coolDownJumpAttack;
        }
        else
        {
            if (Time.time > _delayJumpAttack)
            {
                _isJumpAttack = false;

                if (_canDashing && _withDashEffect)
                    _isDashing = false;
            }
        }

        _anim.SetBool("JumpAttack", _isJumpAttack);
    }

    private void Attack(bool _withDashEffect)
    {
        if (Input.GetKeyDown(OptionsManager.AttackMeleeKey) && !_isJumping
            && Time.time > _delayAttack && !_isCharging)
        {
            _isAttacking = true;

            if (_canDashing && _withDashEffect)
                _isDashing = true;

            _delayAttack = Time.time + _coolDownAttack;
        }
        else
        {
            if (Time.time > _delayAttack)
            {
                if (_canDashing && _withDashEffect)
                    _isDashing = false;
            }
        }

        //_anim.SetBool("Attack", _isAttacking);
    }

    float _delayAfterDash;

    private void Dash()
    {
        if (_canDashing && !_isTouchingWall)
        {
            if (_isDashing)
                _delayAfterDash = Time.time + 0.5f;

            if (Input.GetKeyDown(OptionsManager.DashKey) && !_isCharging &&
                Time.time > _delayDash && Time.time > _delayAfterDash && _dashAmountOnAir > 0)
            {
                if (!_isGrounded)
                    _dashAmountOnAir--;

                _isDashing = true;

                _delayDash = Time.time + _coolDownDash;
            }
            else
            {
                if (Time.time > _delayDash)
                    _isDashing = false;
            }
        }
    }

    private void Throw()
    {
        if (Input.GetKeyDown(OptionsManager.AttackThrowKey)
            && CanThrowAttack() && Time.time > _delayThrow)
        {
            _isThrowing = true;
        }
        else
        {
            if (Time.time > _delayThrow)
                _isThrowing = false;
        }

        _anim.SetBool("Throw", _isThrowing);
    }

    private void ReChargingMana()
    {
        if (!_isGrounded)
            _delaycanRecharge = Time.time + 0.15f;

        if (_isCharging)
            CameraEffect.PlayZoomInOutEffect();

        if (Input.GetKey(OptionsManager.RechargeKey)
            && IsIdling() && Time.time > _delaycanRecharge)
        {
            StartCoroutine(ReChargeMana());
        }
    }

    private void ShowInvincibleShield()
    {
        if (!_isGrounded)
            _delaycanShowShield = Time.time + 0.15f;

        if (_isDefend)
            CameraEffect.PlayZoomInOutEffect();

        if (Input.GetKey(OptionsManager.AttackThrowKey)
            && IsIdling() && Time.time > _delaycanShowShield)
        {
            StartCoroutine(ShowShield());
        }
    }
    #endregion

    #region ABILITY

    #region DASH EFFECT

    private void StartDashing()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);

        _rb.AddForce(
            (_facingRight ? Vector2.right : Vector2.left) * _dashDistance,
            ForceMode2D.Impulse);

        CreateShadowEffect();
        StartCoroutine(ChangeBodyConstraints());
    }

    private IEnumerator ChangeBodyConstraints()
    {
        yield return new WaitForSeconds(0.05f);
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(0.06f);
        _rb.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield break;
    }
    #endregion

    #region SHADOW EFFECT

    private void CreateShadowEffect()
    {
        var _shadowClone = Instantiate(_shadow, transform.position, transform.rotation);
        _shadowClone.transform.localScale = transform.localScale;

        Destroy(_shadowClone, _destroyTimeShadow);

        var _currentSprite = _spriteRenderer.sprite;
        var _shadowSr = _shadowClone.GetComponent<SpriteRenderer>();

        _shadowSr.sprite = _currentSprite;
    }
    #endregion

    #region SELF HEAL
    //belum fix harusnya pakai hold button selama beberapa detik baru ngecharge
    private IEnumerator ReChargeMana() 
    {
        _isCharging = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _anim.SetInteger("ChargeCount", 1);
        // mana bertambah
        yield return new WaitForSeconds(2f);
        _anim.SetInteger("ChargeCount", 2);
        yield return new WaitForSeconds(0.1f);
        _anim.SetInteger("ChargeCount", 0);
        _isCharging = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield break;
    }
    #endregion

    #region SHIELD

    private IEnumerator ShowShield()
    {
        _isDefend = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _anim.SetInteger("ShieldCount", 1);
        //mana berkurang
        yield return new WaitForSeconds(0.5f);
        _anim.SetInteger("ShieldCount", 2);
        yield return new WaitForSeconds(3f); //wait until mana habis
        _anim.SetInteger("ShieldCount", 0);
        _isDefend = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield break;
    }
    #endregion
    #endregion ABILITY

    #region UTILITY

    #region LOAD PLAYER STATS

    private void LoadPlayerData(
        int _healthPoint,
        int _manaPoint,
        int _damageReduction,
        int _damageMelee,
        int _damageMagic,
        int _manaRegen)
    {
        this._healthPoint = _healthPoint;
        this._manaPoint = _manaPoint;
        this._damageReduction = _damageReduction;
        this._damageMelee = _damageMelee;
        this._damageMagic = _damageMagic;
        this._manaRegen = _manaRegen;
    }
    #endregion

    #region Facing Player

    private void HandleFacing()
    {
        if (_horizontal > 0 && !_facingRight)   
        {
            Flip();
        } 
        else if (_horizontal < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion

    #region GROUND CHECK
    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + _groundOffset, Vector2.down, _groundRaycastDistance, _groundLayerMask);
        _isGrounded = hit && !_isDashing;
        _anim.SetBool("isGrounded", _isGrounded);
    }
    #endregion

    #region IS POSSIBLE TO THROW ATTACK

    private bool CanThrowAttack() => !_isMoving &&
                            !_isAttacking &&
                            !_isCharging &&
                            !_isDashing;
    #endregion

    #region WALL CHECK
    private void CheckIsTouchWall()
    {
        _isTouchingWall = Physics2D.OverlapBox(_wallCheckPoint.position, _wallCheckSize, 0, _wallLayerMask);
        _anim.SetBool("isClimb", _isTouchingWall);

        if (_isTouchingWall)
        {
            _isJumping = false;
            _isDashing = false;
            _isThrowing = false;
            _isAttacking = false;
        }
    }

    private void WallSlide()
    {
        _isWallSliding = _isTouchingWall && !_isGrounded && _rb.velocity.y < 0;

        if (_isWallSliding)
            _rb.velocity = new Vector2(_rb.velocity.x, _wallSlideSpeed);

    }
    #endregion

    #region GET LAST POSITION WHEN DIE

    private void SaveLastTransform(float interval)
    {
        if (_isGrounded && Time.time > _delayLastPos)
        {
            _lastPos = transform.position;
            _delayLastPos = Time.time + interval;
        }
    }
    #endregion

    #region CHECK IS IDLE

    private bool IsIdling() => _isGrounded &&
                            !_isMoving     &&
                            !_isAttacking  &&
                            !_isCharging   &&
                            !_isDashing    &&
                            !_isJumping    &&
                            !_isThrowing   &&
                            !_isJumpAttack &&
                            !_isDefend;
    #endregion

    #endregion

    #region DEBUG WITH GIZMOS

    void OnDrawGizmosSelected()
    {
        Debug.DrawLine(transform.position + _groundOffset, transform.position + (Vector3.down * _groundRaycastDistance), Color.red);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_wallCheckPoint.position, _wallCheckSize);
    }
    #endregion

    #region EVENT ANIMATION

    public void MeleeAttack1Event()
    {
        _meleeAttack1.StartMelee();
    }

    public void MeleeAttack2Event()
    {
        _meleeAttack2.StartMelee();
    }

    public void MeleeSpearAttackEvent()
    {
        _meleeSpearAttack.StartMelee();
    }

    public void ThrowFireballEvent()
    {
        Instantiate(_fireBall, _throwPoint.position, Quaternion.identity);
    }

    public void SetFalseAttackEvent() //lanjut besok
    {
        _isAttacking = false;
        _anim.SetBool("Attack", _isAttacking);
        _anim.Play("player idle anim");
    }

    public void SetGotoNextAttackEvent() //besok
    {
        _isAttacking = true;
        _anim.SetBool("Attack", _isAttacking);
        _anim.Play("player attack2 anim");
    }
    #endregion
}
