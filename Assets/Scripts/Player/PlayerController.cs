using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Stats")]
    [SerializeField]
    private float _coolDownAttack;
    [SerializeField]
    private float _coolDownDash;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;

    [Header("Dash & Shadow Effect")]
    [SerializeField]
    private float _dashDistance;
    [SerializeField]
    private int _shadowAmount;
    [SerializeField]
    private float _delayShadow;
    [SerializeField]
    private float _destroyTimeShadow;
    [SerializeField]
    private GameObject _shadow;

    [Header("Ground Raycast")]
    [SerializeField]
    private float _groundRaycastDistance;
    [SerializeField]
    private LayerMask _groundLayerMask;

    [Header("Check Facing")]
    [SerializeField]
    private bool _facingRight = true;

    [Header("Player Anim State")]
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

    private float _horizontal;

    private float _delayAttack;
    private float _delayDash;

    private float _delta;

    private Vector3 _lastPos;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _lastPos = transform.position;

        InputPlayer();

        //buat reset pos, sementara
        if (transform.position.y <= -30f) transform.position = Vector3.zero;
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleFacing();
    }

    #region INPUT

    private void InputPlayer()
    {
        Movement(false);
        Attack(false);
        Jump();
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L) && Time.time > _delayDash)
        {
            for (int i = 0; i < _shadowAmount; i++)
            {
                StartDashing(true, () => CreateShadowEffect());
            }
            _delayDash = Time.time + _coolDownDash;
        }
        else
        {
            if (Time.time > _delayDash) 
                _isDashing = false;
        }
    }

    private void Movement(bool _withShadowEffect)
    {
        if (!_isDashing)
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
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

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded)
            {
                _isJumping = true;
                if (_isJumping)
                {
                    _rb.velocity = Vector2.up * _jumpForce;
                    _isJumping = false;
                }
            }
        }
    }

    private void Attack(bool _withDashEffect)
    {
        if (Input.GetKeyDown(KeyCode.P) && Time.time > _delayAttack)
        {
            _isAttacking = true;
            _anim.SetBool("Attack", _isAttacking);

            if (_withDashEffect)
            {
                for (int i = 0; i < _shadowAmount; i++)
                {
                    StartDashing(true, ()=> CreateShadowEffect());
                }
            }
            _delayAttack = Time.time + _coolDownAttack;
        }
        else
        {
            if (_withDashEffect && Time.time > _delayAttack)
                _isDashing = false;
        }
    }
    #endregion

    #region ABILITY

    #region DASH EFFECT

    private void StartDashing(bool withShadowEffect, Action OnWithShadowEffect = null)
    {
        _isDashing = true;

        transform.position += (_facingRight ? transform.right : -transform.right) * _dashDistance;

        if(_isDashing && withShadowEffect)
        {
            OnWithShadowEffect?.Invoke();
        }
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
    #endregion

    #region UTILITY

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastDistance, _groundLayerMask);
        _isGrounded = hit ? true : false;
        _anim.SetBool("isGrounded", _isGrounded);
    }
    #endregion

    #endregion

    #region DEBUG WITH GIZMOS

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * _groundRaycastDistance), Color.red);
    }
    #endregion

    #region EVENT ANIMATION

    public void SetFalseAttackAnim()
    {
        if (_isAttacking)
        {
            _isAttacking = false;
            _anim.SetBool("Attack", _isAttacking);
        }
    }
    #endregion
}
