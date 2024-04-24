using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFishIdleState : SmallFishGroundedState
{
    public SmallFishIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_SmallFish enemy) : base(_enemyBase,
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
        {

            //if enemy is not moving.
            if (!enemy.CheckMoving())
            {
                if (enemy.IsWallDetected())
                {
                    enemy.Flip();
                }

                stateMachine.ChangeState(enemy.moveState);
            }

        }
    }
}
