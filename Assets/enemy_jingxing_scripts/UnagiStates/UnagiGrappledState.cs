using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnagiGrappledState : UnagiGroundedState
{
    
    public UnagiGrappledState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Unagi enemy) : base(_enemyBase, _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    // Update is called once per frame
    public override void Update()
    {
    }
}
