using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemy_Bucket : Enemy
{

    public GameObject player; //a ref to player. Used for determining which way to fire small fish
    
    #region States
    
    public BucketIdleState idleState { get; private set; }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();

        idleState = new BucketIdleState(this, stateMachine, this);
    }
    
    protected override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void FireFish(int fish_num, bool to_left)
    {
        Debug.Log("Bucket firing to the left = " + to_left);
        //need a fish object too
        //Work on this later. Works intended so far.
    }
}