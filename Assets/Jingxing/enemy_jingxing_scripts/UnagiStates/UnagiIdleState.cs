using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnagiIdleState : UnagiGroundedState
{

    public UnagiIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Unagi enemy) : base(_enemyBase,
        _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);

        CheckIfGrappled();
    }
}
