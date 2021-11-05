using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Melee")]
    [SerializeField]
    private PlayerMelee _melee;

    [Header("Player Stats")]
    [SerializeField]
    private int _healthPoint = 30;
    [SerializeField]
    private int _damageMelee = 10;
    [SerializeField]
    private float _coolDownAttack = 0.25f;
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
    private bool _isCharging;
    [SerializeField]
    private bool _isDashing;
    [SerializeField]
    private bool _isAttacking;
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
    private float _delayThrow;
    private float _delayDash;
    private float _delayLastPos;
    private float _delaycanRecharge;

    private float _delta;

    private Vector3 _lastPos;


    public int HealthPoint { get => _healthPoint; set => _healthPoint = value; }
    public int Damage => _damageMelee;
    public float CoolDownAttack => _coolDownAttack;

    public bool IsAttacking => _isAttacking;
    public bool IsCharging => _isCharging;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _dashFXClone = Instantiate(_dashFX, transform.position, 
            _dashFX.transform.rotation, transform);

        _dashFXClone.SetActive(false);

        //load stats data
        var player = PlayerData.Instance;
        LoadPlayerStats(player.HealPoint, player.DamageMelee,
            player.MoveSpeed, player.CanDoubleJump, player.CanDashing);
    }

    void Update()
    {
        InputPlayer();

        //buat reset pos, sementara
        if (transform.position.y <= -30f) transform.position = _lastPos;

        if (_isGrounded)
        {
            _dashAmountOnAir = 1;
            _jumpAmount = _canDoubleJump ? 2 : 1;
        }
    }

    void FixedUpdate()
    {
        CheckGround();
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
        ReChargingMana();
        Jump();
        Dash();
        Throw();
    }

    private void Movement(bool _withShadowEffect)
    {
        if (!_isCharging)
        {
            if (!_isDashing)
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
        }

        _isJumping = !_isGrounded;
    }

    private void DecreaseJumpAmount() => _jumpAmount--;

    private void Attack(bool _withDashEffect)
    {
        if (Input.GetKeyDown(OptionsManager.AttackMeleeKey) 
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
                _isAttacking = false;

                if (_canDashing && _withDashEffect)
                    _isDashing = false;
            }
        }

        _anim.SetBool("Attack", _isAttacking);

    }

    float _delayAfterDash;
    private void Dash()
    {
        if (_canDashing)
        {
            if (_isDashing) //setelah dash maka akan ada delay 0.5 detik untuk melakukan dash selanjutnya
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
            && _isIdling() && Time.time > _delayThrow)
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

        if(_isCharging)
            CameraEffect.PlayZoomInOutEffect();

        if (Input.GetKey(OptionsManager.RechargeKey) 
            && _isIdling() && Time.time > _delaycanRecharge)
        {
            StartCoroutine(RechargeMana());
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

    #region Charging Mana
    
    private IEnumerator RechargeMana()
    {
        _isCharging = true;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        _anim.SetInteger("ChargeCount", 1);
        yield return new WaitForSeconds(2f);
        _anim.SetInteger("ChargeCount", 2);
        yield return new WaitForSeconds(0.1f);
        _anim.SetInteger("ChargeCount", 0);
        _isCharging = false;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield break;
    }
    #endregion
    #endregion

    #region UTILITY

    #region LOAD PLAYER STATS

    private void LoadPlayerStats(int _healthPoint, 
        int _damage, float _moveSpeed, bool _canDoubleJump, bool _canDashing)
    {
        this._healthPoint = _healthPoint;
        this._damageMelee = _damage;
        this._moveSpeed = _moveSpeed;
        this._canDoubleJump = _canDoubleJump;
        this._canDashing = _canDashing;
    }
    #endregion

    #region Facing Player

    private void HandleFacing()
    {
        if (_horizontal > 0 && !_facingRight) Flip();
        else if (_horizontal < 0 && _facingRight) Flip();
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
        _isGrounded = hit ? true : false;
        _anim.SetBool("isGrounded", _isGrounded);
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

    private bool _isIdling() => _isGrounded  &&
                            !_isMoving    &&
                            !_isAttacking &&
                            !_isCharging  &&
                            !_isDashing   &&
                            !_isJumping   &&
                            !_isThrowing;
    #endregion

    #endregion

    #region DEBUG WITH GIZMOS

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + _groundOffset, transform.position + (Vector3.down * _groundRaycastDistance), Color.red);
    }
    #endregion

    #region EVENT ANIMATION

    public void MeleeToEnemyEvent()
    {
        _melee.StartMelee();
    }
    #endregion
}
