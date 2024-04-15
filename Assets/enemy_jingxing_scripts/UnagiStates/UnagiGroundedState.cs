using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnagiGroundedState : EnemyState //this is the foundation for other states.
{
    protected scrEnemy_Unagi enemy;
    protected Transform player;
    public UnagiGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Unagi enemy) : base(_enemyBase, _stateMachine)
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //nothing done for below yet.
        if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
        {
        }
    }
}
