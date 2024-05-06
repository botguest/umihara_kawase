using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //player.Addforce(0, player.jumpForce);
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        stateTimer = .4f;
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

        if (Input.GetKeyDown(KeyCode.W) && player.isUpLadderDetected() && stateTimer < 0)
            stateMachine.ChangeState(player.ladderState);

    }

}
