using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Object;

public class UnagiBabyMoveState : UnagiBabyGroundedState
{

    public UnagiBabyMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_UnagiBaby enemy) : base(_enemyBase, _stateMachine, enemy)
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
        base.Update();
        
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected())
        {
            enemy.DestroyShortCut();
        }
    }
}
