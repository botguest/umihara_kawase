using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player : Entity
{
    public bool isBusy { get; private set;}

    [Header("Move Info")]
    public float moveSpeed;
    public float jumpForce;
    private bool grapple = false;
    public Collider2D col;

    public void GrappleEnemy()
    {
        grapple = true;
    }

    public void NotGrappleEnemy()
    {
        grapple = false;
    }


    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerGrappleState grappleState { get; private set; }    
    public PlayerDeadState deadState { get; private set; }
    public PlayerLadderState ladderState { get; private set; }
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
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
        ladderState = new PlayerLadderState(this, stateMachine, "ladder");
        col = GetComponent<Collider2D>();

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
        Debug.Log(stateMachine.currentState);
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishedTrigger();
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "enemy") //also detect if a certain boolean in the enemy script is true (stunned/not stunned)
        {
            if(grapple == true)
            {
                stateMachine.ChangeState(grappleState);
            }
            else
            {
                stateMachine.ChangeState(deadState);
            }
        }
    }


}
