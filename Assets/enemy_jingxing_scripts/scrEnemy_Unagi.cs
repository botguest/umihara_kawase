using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemy_Unagi : Enemy
{
    
    #region States
    
    public UnagiMoveState moveState { get; private set; }
    public UnagiIdleState idleState {  get; private set; }
    public UnagiAttackState attackState {  get; private set; }
    //public SkeletonBattleState battleState { get; private set; }

    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new UnagiIdleState(this, stateMachine, this);
        moveState = new UnagiMoveState(this, stateMachine, this);
        attackState = new UnagiAttackState(this, stateMachine, this); //not yet worked on
        //battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
    
    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
