using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondAttack : PlayerState
{
    public PlayerSecondAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.secondAnim.Play("SW");
        stateTimer = 0.055f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
            player.secondAnim.Play("default");
            return;
        }
        player.SetVelocity(0, 0);
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
