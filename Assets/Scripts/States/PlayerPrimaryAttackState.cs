using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.secondAnim.Play("SW");
        stateTimer = 0.085f;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, 0);

        if (stateTimer < 0 && Input.GetKeyDown(KeyCode.Space))
        {
            comboWindow = false;
            stateMachine.ChangeState(player.jumpState);
            player.secondAnim.Play("default");
            return;
        }
        if (comboWindow && Input.GetKey(KeyCode.Mouse0))
        {
            comboWindow = false;
            stateMachine.ChangeState(player.SecondAttackState);
        }
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

    }
}
