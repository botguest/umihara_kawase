using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerState
{
    private bool canCollide = false;
    public PlayerLadderState(Player player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        
        rb.gravityScale = 0;
        stateTimer = .3f;
        player.col.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 9.8f;
        player.col.enabled = true;
        rb.AddForce(new Vector2(player.facingDir * 30, 60), ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();


        if (xInput != 0 && !player.IsWallDetected()) //if player moves horizontally and no wall is beside him
        {
            stateMachine.ChangeState(player.moveState);
        }

        if(!player.isDownLadderDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.SetVelocity(0, yInput * player.moveSpeed);


    }
}
