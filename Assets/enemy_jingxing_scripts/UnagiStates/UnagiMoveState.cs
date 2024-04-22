using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnagiMoveState : UnagiGroundedState
{
    public UnagiMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Unagi enemy) : base(_enemyBase, _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        //throw fish after specific sec
        stateTimerThrowFish = enemy.throwFishTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        
        //if it is time to throw fish
        //add another check for if thrown already.
        if (stateTimerThrowFish < 0)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
    }
}
