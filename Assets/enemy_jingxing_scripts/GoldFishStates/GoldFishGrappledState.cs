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
    }

    public override void Exit()
    {
        base.Exit();
    }

    // Update is called once per frame
    public override void Update()
    {
    }
}
