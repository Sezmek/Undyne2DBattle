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
        player.SetVelocity(rb.velocity.x * 0.5f, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0 && Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            player.SetVelocity(rb.velocity.x, rb.velocity.y + (player.jumpForceMultiplier * Time.deltaTime));
        }
        else
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
 