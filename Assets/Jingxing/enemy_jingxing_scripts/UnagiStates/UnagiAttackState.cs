using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnagiAttackState : UnagiGroundedState
{
    
    public UnagiAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Unagi enemy) : base(_enemyBase, _stateMachine, enemy)
    {
    }
    
    //Haven't Worked On. Working Now.
    
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
        
        //spawn
        enemy.InstantiateBaby();
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
