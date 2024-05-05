using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GoldFish : Enemy
{
    #region States
    public GoldFishIdleState idleState {  get; private set; }
    public GoldFishMoveState moveState { get; private set; }
    public GoldFishGrappledState grappledState { get; private set; }
    //public SkeletonBattleState battleState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new GoldFishIdleState(this, stateMachine, this);
        moveState = new GoldFishMoveState(this, stateMachine, this);
        grappledState = new GoldFishGrappledState(this, stateMachine, this);
        //battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    
    //need a on collisionEnter
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && IsMyStateGrappled<GoldFishGrappledState>())
        {
            Destroy(gameObject);
        }
    }
}
