using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool isBusy { get; private set;}

    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;

    


    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerGrappleState grappleState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine(); 

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        grappleState = new PlayerGrappleState(this, stateMachine, "Grapple");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishedTrigger();
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }
}
