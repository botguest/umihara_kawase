using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketIdleState : BucketGroundedState
{
    bool fire_to_left;
    
    public BucketIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Bucket enemy) : base(_enemyBase,
        _stateMachine, enemy)
    {
    }
    
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime; //idle time is how long per fish shoot
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
            if (player.transform.position.x <= enemy.transform.position.x)
            {
                fire_to_left = true;
            }
            else
            {
                fire_to_left = false;
            }
            //fire it multiple times. Within an interval of several seconds
            enemy.FireFishRepeatedlyStarter(fire_to_left);
            
            //reset stateTimer & generate three small fish
            stateTimer = enemy.idleTime;
        }


    }
}
