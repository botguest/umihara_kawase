using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketGrappledState : BucketGroundedState
{

    public BucketGrappledState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Bucket enemy) : base(_enemyBase, _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        stateTimer = enemy.shockTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        CheckIfRecovered();
        CheckIfGrappled();
    }
}
