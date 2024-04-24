using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFishMoveState : SmallFishGroundedState
{
    public SmallFishMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_SmallFish enemy) : base(_enemyBase, _stateMachine, enemy)
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

    public override void Update()
    {
        base.Update();
        
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected())
        {
            enemy.Flip();
        }
    }
}
