using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;


    protected bool comboWindow;
    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected bool triggerCalled;

    protected float stateTimer;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;  
    }
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.Rb;
        player.slidePS.Stop();
        triggerCalled = false;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public virtual void Trigger()
    {
        if ( comboWindow )
        {
            comboWindow = false;
            return;
        }
        comboWindow = true;
    }
}
