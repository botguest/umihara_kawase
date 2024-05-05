using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerState
{
    public PlayerLadderState(Player player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        rb.gravityScale = 0;
        stateTimer = .3f;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 9.8f;
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            if(xInput != 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

        player.SetVelocity(0, yInput * player.moveSpeed);

    }
}
