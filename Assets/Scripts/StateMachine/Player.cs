using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Attack Info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public bool canMove = true;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTime;
    public float dashDir { get; private set; }

    [Header("Jump Info")]
    public float jumpForce;      
    public float maxJumpTime;      
    public float jumpForceMultiplier;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask WhatIsGround;

    [SerializeField] private int hp = 5;
    [SerializeField] List<GameObject> hpHearts;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;
    private bool isBusy;

    #region Components
    public Animator anim {  get; private set; }
    public Animator secondAnim { get; private set; }
    public ParticleSystem slidePS { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public PlayerStateMachine stateMachine {  get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    #endregion

    #region States
    public PlayerIdleState idleState { get; private set; }
    public PlayerPrimaryAttackState FirstAttackState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerSecondAttack SecondAttackState { get; private set; }
    public PlayerTutorialState TutorialState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        FirstAttackState = new PlayerPrimaryAttackState(this, stateMachine, "FirstAttack");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Slide");
        SecondAttackState = new PlayerSecondAttack(this, stateMachine, "SecondAttack");
        TutorialState = new PlayerTutorialState(this, stateMachine, "Grabbed");

    }
    private void Start()
    {
        slidePS = GetComponentInChildren<ParticleSystem>();
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        Animator[] animators = GetComponentsInChildren<Animator>();
        if (animators.Length > 1)
        {
            anim = animators[0];
            secondAnim = animators[1];
        }
        Rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        if (stateMachine.currentState == wallSlideState)
            return;
        dashCooldownTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime < 0 && canMove  )
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
            dashCooldownTime = dashCooldown;
        }
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        FlipController(_xVelocity);
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.down, wallCheckDistance, WhatIsGround);

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, WhatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x - wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

    }

    private IEnumerator Busy(float _seconds)
    {
        isBusy = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(_seconds);
        spriteRenderer.color = Color.white;
        isBusy = false;
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, -180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    public void Damage()
    {
        if (stateMachine.currentState == dashState || isBusy == true)
            return;
        StartCoroutine(Busy(1));
        hpHearts[hp-1].GetComponent<HpHeart>().DestroyAnimTrigger();
        hp -= 1;
        if (hp == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void Trigger() => stateMachine.currentState.Trigger();
}
