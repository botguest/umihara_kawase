using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGrappledState : GoldFishGroundedState
{
    public GoldFishGrappledState(Enemy _enemyBase, EnemyStateMachine _stateMachine, Enemy_GoldFish enemy) : base(_enemyBase, _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entered Goldfish Grappled State!!");
        stateTimer = enemy.shockTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    // Move with the player
    //After certain time, go back to idle
    //if grappled go back to grappled
    public override void Update()
    {

        
        
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        
        CheckIfGrappled();
        
        //get move dir
    }

    private void GetGrappledMoveDir()
    {
        
    }
}
