using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(xInput * player.moveSpeed, player.Rb.linearVelocity.y);

        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);

    }
}
