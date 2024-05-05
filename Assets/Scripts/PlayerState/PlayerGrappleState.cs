using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrappleState : PlayerState
{
    public PlayerGrappleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {

        base.Enter();

        stateTimer = 1f;


        player.GetComponent<GrapplingHook2>().enabled = false;
        //play some grappled sfx
    }

    public override void Exit()
    {
        base.Exit();

        player.NotGrappleEnemy();
        player.GetComponent<GrapplingHook2>().enabled = true;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            player.SetZeroVelocity();
        }
    }
}
