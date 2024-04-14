using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFishGroundedState : EnemyState
{
    protected Enemy_GoldFish enemy;
    protected Transform player;
    public GoldFishGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, Enemy_GoldFish enemy) : base(_enemyBase, _stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
        {
        }
    }
}
