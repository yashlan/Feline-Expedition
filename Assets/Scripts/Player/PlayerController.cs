using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Icon Mini Map")]
    [SerializeField]
    private GameObject playerIcon;

    [Header("Melee")]
    [SerializeField]
    private PlayerMelee _meleeAttack1;
    [SerializeField]
    private PlayerMelee _meleeAttack2;
    [SerializeField]
    private PlayerMelee _meleeSpearAttack1;
    [SerializeField]
    private PlayerMelee _meleeSpearAttack2;
    [SerializeField]
    private PlayerMelee _meleeSpearAttack3;
    [SerializeField]
    private PlayerMelee _meleeSpearAttack4;

    [Header("Fireball")]
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    private Transform _throwPoint;

    [Header("Combo Attack")]
    [SerializeField]
    private bool _isTimeComboAttack;
    [SerializeField]
    private float _timeToResetCombo;
    [SerializeField]
    private int _comboAttackCount;

    [Header("Player Stats")]
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _healthPoint;
    [SerializeField]
    private float _manaPoint;
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
    private bool _isTalking;
    [SerializeField]
    private bool _isDead;
    [SerializeField]
    private bool _isHurt;
    [SerializeField]
    private bool _isThrowing;
    [SerializeField]
    private bool _isDefend;
    [SerializeField]
    private bool _isSelfHeal;
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
    [SerializeField]
    private bool _isShopping;
    [SerializeField]
    private bool _isRest;

    private BoxCollider2D _boxCollider;
    private CircleCollider2D _circleCollider;
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
    private float _delaycanSelfHeal;
    private float _delaycanShowShield;
    private float _delta;
    private float _delayAfterDash;
    private Vector3 _lastPos;
    private bool _wasFirstJump = false;

    #region GETTER SETTER
    public Animator Anim { get => _anim; set => _anim = value; }
    public Rigidbody2D Rigidbody { get => _rb; set => _rb = value; }
    public int HealthPoint { get => _healthPoint; set => _healthPoint = value; }
    public int DamageReduction { get => _damageReduction; set => _damageReduction = value; }
    public int Coins { get => _coins; set => _coins = value; }
    public int DamageMelee => _damageMelee;
    public int DamageMagic => _damageMagic;
    public float ManaPoint { get => _manaPoint; set => _manaPoint = value; }
    public float CoolDownAttack => _coolDownAttack;
    public float CoolDownSpearAttack => _coolDownSpearAttack;
    public bool IsTimeComboAttack { get => _isTimeComboAttack; set => _isTimeComboAttack = value; }
    public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
    public bool IsSelfHeal { get => _isSelfHeal; set => _isSelfHeal = value; }
    public bool IsTalking { get => _isTalking; set => _isTalking = value; }
    public bool IsDefend { get => _isDefend; set => _isDefend = value; }
    public bool FacingRight => _facingRight;
    public bool IsGrounded => _isGrounded;
    public bool IsHurt => _isHurt;
    public bool IsDead => _isDead;
    public bool IsShopping { get => _isShopping; set => _isShopping = value; }
    public bool IsRest { get => _isRest; set => _isRest = value; }

    #endregion


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();

        //load stats data
        LoadPlayerData(
            PlayerData.HealthPoint,
            PlayerData.ManaPoint,
            PlayerData.DamageReduction,
            PlayerData.DamageMelee,
            PlayerData.DamageMagic,
            PlayerData.ManaRegen,
            PlayerData.Coins);

        StartCoroutine(SetupUI());

        if (GameManager.SceneType == SceneType.map_1)
        {
            StartCoroutine(SetupOnNewGame(2f));
        }

        playerIcon.transform.localPosition = Vector3.zero;
    }

    IEnumerator SetupOnNewGame(float timeToStop)
    {
        yield return new WaitUntil(() => GameManager.GameState == GameState.Playing);
        while (timeToStop > 0)
        {
            _horizontal = -1;
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
            _anim.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
            _isMoving = _anim.GetFloat("Speed") != 0f;
            timeToStop -= Time.deltaTime;
            yield return null;
        }

        if (timeToStop <= 0)
        {
            CameraEffect.PlayZoomIn(20, (x) =>
            {
                GameObject.Find("trigger move").transform.GetChild(0).gameObject.SetActive(true);
                TutorialManager.Instance.hasShowMoveKey = true;
            });
            yield break;
        }
    }

    IEnumerator SetupUI()
    {
        yield return new WaitUntil(() => GameManager.GameState == GameState.Playing);
        PlayerManaUI.UpdateUI();
        PlayerCoinsUI.UpdateUI();
        SliderHealthPlayerUI.UpdateUI();

        if (OptionsManager.DisplayPlayerUI)
            PlayerRuneUI.Show();
        else
            PlayerRuneUI.Hide();
    }

    void Update()
    {
        if (_isDead || _isTalking || _isShopping || _isRest)
            return;

        if (GameManager.GameState == GameState.Playing)
        {
            InputPlayer();
            CheckIsTouchWall();
            CheckGround();
            HandleAttackCombo();

            if (_isGrounded || _isTouchingWall)
            {
                _dashAmountOnAir = 1;

                if (GameManager.SceneType == SceneType.map_1)
                {
                    _canDoubleJump = TutorialManager.Instance.hasShowDoubleJumpKey;
                }
                _jumpAmount = _canDoubleJump ? 2 : 1;
            }
        }
    }

    void FixedUpdate()
    {
        if (_isDead || _isTalking || _isShopping || _isRest)
        {
            FreezePosition();
            return;
        }

        if (GameManager.GameState == GameState.Playing)
        {
            WallSlide();
            HandleFacing();
            HandleFrictionPhysicsMaterial();
            SaveLastTransform(0.5f);

            if (_isDashing)
                StartDashing();
        }
    }

    #region INPUT

    private void InputPlayer()
    {
        if(GameManager.SceneType == SceneType.map_1)
        {
            if (TutorialManager.Instance.hasShowMoveKey)
            {
                Movement(false);
            }

            if (TutorialManager.Instance.hasShowAttackMeleeKey)
            {
                Attack(false);
            }

            if (TutorialManager.Instance.hasShowAttackMeleeKey && 
                TutorialManager.Instance.hasShowJumpKey)
            {
                JumpAttack(false);
            }

            if (TutorialManager.Instance.hasShowSelfHealKey)
            {
                SelfHeal();
            }

            if (TutorialManager.Instance.hasShowJumpKey)
            {
                Jump();
            }

            if (TutorialManager.Instance.hasShowDashKey)
            {
                Dash();
            }

            if (PlayerData.IsInvincibleShieldUsed())
            {
                ShowInvincibleShield();
            }
            else
            {
                if (TutorialManager.Instance.hasShowAttackThrowKey)
                {
                    Throw();
                }
            }
        }
        else
        {
            Movement(false);
            Attack(false);
            JumpAttack(false);
            SelfHeal();
            Jump();
            Dash();

            if (PlayerData.IsInvincibleShieldUsed())
                ShowInvincibleShield();
            else
                Throw();
        }
    }

    private void Movement(bool _withShadowEffect)
    {
        if (!_isHurt)
        {
            if (!_isSelfHeal || !_isDefend || !_isAttacking)
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
    }

    private void Jump()
    {
        if (Input.GetKeyDown(OptionsManager.JumpKey) && (!_isSelfHeal || !_isDefend))
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
            else if (((Input.GetKey(OptionsManager.LeftKey) && _facingRight) ||
                (Input.GetKey(OptionsManager.RightKey)      && !_facingRight)) && 
                (_isWallSliding || _isTouchingWall)         && _jumpAmount > 0)
            {
                _rb.velocity = Vector2.zero;
                _rb.velocity = Vector2.up * _wallJumpForce;

                _wasFirstJump = true;
                Invoke(nameof(DecreaseJumpAmount), 0.1f);
            }
            else if (((Input.GetKey(OptionsManager.LeftKey) && _facingRight) ||
                (Input.GetKey(OptionsManager.RightKey)      && !_facingRight)) &&
                (_isWallSliding || _isTouchingWall)         && _jumpAmount == 1)
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
            && Time.time > _delayJumpAttack && !_isSelfHeal)
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

        _anim.SetBool(PlayerData.IsWaterSpearUsed() ? "JumpAttackSpear" : "JumpAttack", _isJumpAttack);
    }

    private void Attack(bool _withDashEffect)
    {
        if (Input.GetKeyDown(OptionsManager.AttackMeleeKey) && !_isJumping && !_isDashing && !_isSelfHeal
            && Time.time > _delayAttack)
        {
            _timeToResetCombo += _coolDownAttack;

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

        _isAttacking = _isTimeComboAttack;
    }

    private void Dash()
    {
        if (_canDashing && !_isTouchingWall && !_isAttacking)
        {
            if (_isDashing)
                _delayAfterDash = Time.time + 0.5f;

            if (Input.GetKeyDown(OptionsManager.DashKey) && !_isSelfHeal &&
                Time.time > _delayDash && Time.time > _delayAfterDash && _dashAmountOnAir > 0)
            {
                if (!_isGrounded)
                    _dashAmountOnAir--;

                _isDashing = true;

                AudioManager.PlaySfx(AudioManager.Instance.PlayerDashClip);

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
            && CanThrowAttack() && Time.time > _delayThrow && _manaPoint > 0)
        {
            _manaPoint -= 5;
            PlayerManaUI.UpdateUI();
            _isThrowing = true;
        }
        else
        {
            if (Time.time > _delayThrow)
                _isThrowing = false;
        }

        _anim.SetBool("Throw", _isThrowing);
    }

    private void SelfHeal()
    {
        if (!_isGrounded)
            _delaycanSelfHeal = Time.time + 0.15f;

        var maxHealth = SliderHealthPlayerUI.Instance.sliderHP.maxValue;

        if (Input.GetKeyDown(OptionsManager.SelfHealKey)
            && IsIdle() && Time.time > _delaycanSelfHeal && _manaPoint > 0 && _healthPoint < maxHealth)
        {
            StartCoroutine(ISelfHeal());
        }
    }

    private void ShowInvincibleShield()
    {
        if (!_isGrounded)
            _delaycanShowShield = Time.time + 0.15f;

        if(_isGrounded && _manaPoint > 0)
        {
            if (Input.GetKeyDown(OptionsManager.AttackThrowKey) && Time.time > _delaycanShowShield)
            {
                StartCoroutine(ShowShield());
            }
            else if (Input.GetKeyUp(OptionsManager.AttackThrowKey))
            {
                StopCoroutine(ShowShield());
                _anim.SetInteger("ShieldCount", 0);
            }
        }
    }

    #endregion INPUT

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
    private IEnumerator ISelfHeal() 
    {
        var maxHealth = SliderHealthPlayerUI.Instance.sliderHP.maxValue;

        while (_manaPoint > 0 && _healthPoint <= maxHealth && !_isHurt)
        {
            _anim.SetInteger("SelfHealCount", 1);
            yield return null;
        }

        if (_isHurt)
        {
            _anim.SetInteger("SelfHealCount", 0);
            yield break;
        }

        if (HealthPoint >= maxHealth)
        {
            HealthPoint = (int)maxHealth;
            _anim.SetInteger("SelfHealCount", 0);
            yield break;
        }

        if (_manaPoint <= 0)
        {
            _manaPoint = 0;
            _anim.SetInteger("SelfHealCount", 0);
            yield break;
        }
    }
    #endregion

    #region SHIELD

    private IEnumerator ShowShield()
    {
        _anim.SetInteger("ShieldCount", 1);
        yield return new WaitForSeconds(0.5f);
        _anim.SetInteger("ShieldCount", 2);
    }
    #endregion

    #endregion ABILITY

    #region UTILITY

    #region KNOCKBACK

    public void KnockBack(float force, Transform enemy)
    {
        _isHurt = true;
        _anim.SetBool("Hurt", _isHurt);
        PanelHurtUIController.Instance.Show();
        StartCoroutine(IKnockBack(force, enemy));
    }

    IEnumerator IKnockBack(float force, Transform enemy)
    {
        if (IsDead)
            yield break;

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        var dir = (transform.position - enemy.position).normalized;
        dir.y = 0.5f;

        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, dir.y);

        Rigidbody.AddForce(dir * force);
        yield return new WaitForSeconds(0.02f);
    }
    #endregion

    #region LOAD PLAYER STATS

    private void LoadPlayerData(
        int _healthPoint,
        float _manaPoint,
        int _damageReduction,
        int _damageMelee,
        int _damageMagic,
        int _manaRegen,
        int _coins)
    {
        this._healthPoint = _healthPoint;
        this._manaPoint = _manaPoint;
        this._damageReduction = _damageReduction;
        this._damageMelee = _damageMelee;
        this._damageMagic = _damageMagic;
        this._manaRegen = _manaRegen;
        this._coins = _coins;
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
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + _groundOffset, 
            Vector2.down, 
            _groundRaycastDistance, 
            _groundLayerMask);

        _isGrounded = hit && !_isDashing;
        _anim.SetBool("isGrounded", _isGrounded);
    }
    #endregion

    #region IS POSSIBLE TO THROW ATTACK

    private bool CanThrowAttack() => !_isMoving &&
                            !_isAttacking &&
                            !_isSelfHeal &&
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

    #region ON DEAD AREA

    public void OnHitDeadArea()
    {
        if(_healthPoint <= 0)
        {
            Dead();
            return;
        }

        GameManager.ChangeGameState(GameState.HitDeadArea);

        if (GameManager.GameState == GameState.HitDeadArea && !_isDead)
        {
            _isDead = true;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _healthPoint -= 5;
            SliderHealthPlayerUI.UpdateUI();
            PanelSlideUIController.Instance.FadeIn(() => GetLastPos(), true);
        }
    }

    private void GetLastPos()
    {
        _isDead = false;
        GameManager.GameState = GameState.Playing;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.position = _lastPos;

        if (FindObjectsOfType<Enemy>() == null)
            return;

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.ResetPosition();
        }
    }
    #endregion

    #region ON DEAD HEALTHPOINT <= 0 (GAME OVER)

    public void Dead()
    {
        GameManager.ChangeGameState(GameState.GameOver);

        if (GameManager.GameState == GameState.GameOver && !_isDead)
        {
            _isDead = true;
            _anim.SetTrigger("Dead");
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke(nameof(ShowGameOverText), 2f);
            PanelSlideUIController.Instance.FadeIn(() => Restart(), 5f);
        }
    }

    private void ShowGameOverText()
    {
        GameManager.ShowGameOverText();
    }

    private void Restart()
    {
        PlayerData.SetDefaultValue();
        SceneManager.LoadScene(PlayerData.LastScene);
    }
    #endregion

    #region SAVE TRANSFORM

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

    public bool IsIdle() => _isGrounded    &&
                            !_isMoving     &&
                            !_isAttacking  &&
                            !_isSelfHeal   &&
                            !_isDashing    &&
                            !_isJumping    &&
                            !_isThrowing   &&
                            !_isJumpAttack &&
                            !_isHurt       &&
                            !_isDefend;
    #endregion

    #region HANDLE ATTACK COMBO

    private void HandleAttackCombo()
    {
        _isTimeComboAttack = _timeToResetCombo > 0;

        if (_isTimeComboAttack)
        {
            if(_timeToResetCombo >= 0)
            {
                _timeToResetCombo -= 1f * Time.deltaTime;

                if(_timeToResetCombo <= 0)
                {
                    _isTimeComboAttack = false;
                    _timeToResetCombo = 0;
                }
            }
        }

    }
    #endregion

    #region HANDLE PHYSIC MATERIAL

    private void HandleFrictionPhysicsMaterial()
    {
        _rb.sharedMaterial.friction = 
        _circleCollider.sharedMaterial.friction = 
        _boxCollider.sharedMaterial.friction = !_isGrounded ? 0f : 0.5f;
    }
    #endregion

    #region FREEZE/UNFREEZE RIGIDBODY WHEN TOUCH TRANSITION AREA OR TALK WITH NPC

    public static void FreezePosition()
    {
        Instance._rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public static void UnFreezePosition()
    {
        Instance._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    #endregion

    #region INCREASE MANA WHEN HIT ENEMY

    public void IncreaseMana(int amount)
    {
        var maxManaPoint = PlayerData.DEFAULT_MANAPOINT + PlayerData.ManaPointExtra;

        if(_manaPoint >= maxManaPoint)
        {
            _manaPoint = maxManaPoint;
            PlayerManaUI.UpdateUI();
            return;
        }
        _manaPoint += amount;
        PlayerManaUI.UpdateUI();
    }
    #endregion

    #region UPDATE COINS
    public void AddCoin(int amount)
    {
        Coins += amount;
    }
    #endregion

    #endregion UTILITY

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

    public void MeleeSpearAttack1Event()
    {
        _meleeSpearAttack1.StartMelee();
    }

    public void MeleeSpearAttack2Event()
    {
        _meleeSpearAttack2.StartMelee();
    }

    public void MeleeSpearAttack3Event()
    {
        _meleeSpearAttack3.StartMelee();
    }

    public void MeleeSpearAttack4Event()
    {
        _meleeSpearAttack4.StartMelee();
    }

    public void SelfHealEvent()
    {
        if(_manaPoint <= 0)
        {
            _manaPoint = 0;
            PlayerManaUI.UpdateUI();
            SliderHealthPlayerUI.UpdateUI();
            return;
        }

        _manaPoint -= 10;
        _healthPoint += 3;
        PlayerManaUI.UpdateUI();
        SliderHealthPlayerUI.UpdateUI();
    }

    public void ThrowFireballEvent()
    {
        AudioManager.PlaySfx(AudioManager.Instance.PlayerThrowClip);
        Instantiate(_fireBall, _throwPoint.position, Quaternion.identity);
    }

    public void PlayWaterSpearAttack4Event() //event for attack spear 3 anim
    {
        _anim.Play("player water spear 4 anim");
    }

    public void DisableHurtEvent()
    {
        _isHurt = false;
        _anim.SetBool("Hurt", _isHurt);
    }

    public void ShieldEvent()
    {
        if (_manaPoint <= 0)
        {
            _manaPoint = 0;
            _anim.SetInteger("ShieldCount", 0);
            PlayerManaUI.UpdateUI();
            return;
        }

        _manaPoint -= 3;
        PlayerManaUI.UpdateUI();
    }
    #endregion
}
