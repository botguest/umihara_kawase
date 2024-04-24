using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        player.GetComponent<Collider2D>().enabled = false;
        rb.gravityScale = 0;

    }

    public override void Exit()
    {
        base.Exit();
    }
}
