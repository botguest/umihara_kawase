using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;


    //Give the current state a more readable name to be used in code
    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected bool triggerCalled;


    protected float stateTimer; //this can be set to any number in one particular state. A handy timer.

    public PlayerState(Player _player, PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    //These 3 functions are used to be overwritten by the current state.
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true); //Play the assigned animation
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishedTrigger()
    {
        triggerCalled = true;
    }

    

}

