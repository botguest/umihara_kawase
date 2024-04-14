using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishIdleState : GoldFishGroundedState
{
    public GoldFishIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, Enemy_GoldFish enemy) : base(_enemyBase, _stateMachine, enemy)
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

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);


    }
}
