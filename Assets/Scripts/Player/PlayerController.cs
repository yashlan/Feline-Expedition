using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Stats")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;

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
    private int _attackCount;
    [SerializeField]
    private bool _isAttacking;
    [SerializeField]
    private bool _isGrounded;
    [SerializeField]
    private bool _isMoving;
    [SerializeField]
    private bool _isJumping;

    private Rigidbody2D _rb;
    private Animator _anim;

    private float _horizontal;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
    }

    void FixedUpdate()
    {
        CheckGround();
        HandleFlip();
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastDistance, _groundLayerMask);

        if (hit) 
            _isGrounded = true;
        else
            _isGrounded = false;

        _anim.SetBool("isGrounded", _isGrounded);
    }


    #region INPUTAN

    private void InputPlayer()
    {
        Movement();
        Jump();
        Attack();
    }

    private void Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _anim.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
        _anim.SetFloat("vSpeed", _rb.velocity.y);
        _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
        _isMoving = _anim.GetFloat("Speed") == 0f ? false : true;
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

    private void Attack()
    {
        if (Input.GetKey(KeyCode.P))
        {
            _isAttacking = true;
            _anim.SetBool("Attack", true);
        }
    }

    #endregion

    private void HandleFlip()
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

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3.down * _groundRaycastDistance), Color.red);
    }

    #region EVENT ANIMATION

    public void SetFalseAttackAnim()
    {
        _isAttacking = false;
        _anim.SetBool("Attack", false);
    }

    #endregion
}
