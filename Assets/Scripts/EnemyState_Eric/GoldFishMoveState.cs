using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishMoveState : GoldFishGroundedState
{
    public GoldFishMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, Enemy_GoldFish enemy) : base(_enemyBase, _stateMachine, enemy)
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

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }

        CheckIfGrappled();
    }
}
