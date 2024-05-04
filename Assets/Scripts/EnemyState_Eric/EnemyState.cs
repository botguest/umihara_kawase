using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    private string animBoolName;

    protected float stateTimer;
    //the timer for throw_fish for bucket & unagi
    protected float stateTimerThrowFish;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        stateTimerThrowFish -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
    }
}
