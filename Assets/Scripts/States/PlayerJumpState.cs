using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.maxJumpTime;
        player.SetVelocity(rb.linearVelocity.x * 0.5f, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(rb.linearVelocity.x, rb.linearVelocity.y * 0.6f);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0 && Input.GetKey(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            player.SetVelocity(rb.linearVelocity.x, rb.linearVelocity.y + (player.jumpForceMultiplier * Time.deltaTime));
        }
        else
        {
            stateMachine.ChangeState(player.airState);
        }
        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed * .8f, player.Rb.linearVelocity.y);

    }
}
 