using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetZeroVelocity();
        rb.gravityScale = 2;
        rb.mass = 100;
        player.die.Play();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled == true)
        {
            SceneManager.LoadScene(player.field1);
        }
    }



    public override void Exit()
    {
        base.Exit();

        
    }
}
