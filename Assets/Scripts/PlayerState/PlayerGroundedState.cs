using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .7f;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);


        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) 
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.W) && player.isLadderDetected() && stateTimer < 0)
            stateMachine.ChangeState(player.ladderState);
    }
}
