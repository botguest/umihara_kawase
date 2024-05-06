using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketGroundedState : EnemyState
{
    protected scrEnemy_Bucket enemy;
    protected Transform player;
    public BucketGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, scrEnemy_Bucket enemy) : base(_enemyBase, _stateMachine)
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
    
    public void CheckIfGrappled() //present in every GroundedState.
    {
        //Debug.Log("");
        if (enemy.grappled && base.stateMachine.currentState != enemy.grappledState) //if grappled and current state is not grappled.
        {
            stateMachine.ChangeState(enemy.grappledState);
        }
        else
        {
            Debug.Log("Not Grappled?");
        }
    }

    public void CheckIfRecovered() //present in every GroundedState.
    {
        if (stateTimer < 0)
        {
            enemy.grappled = false;
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}