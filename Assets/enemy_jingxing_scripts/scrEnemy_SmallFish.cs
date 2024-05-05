using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Haven't Setup. Work on later. No states defined.
public class scrEnemy_SmallFish : Enemy
{

    [Header("smallfish-specific Info")] 
    public float hitForce;
    
    #region States
    
    public SmallFishMoveState moveState { get; private set; }
    public SmallFishIdleState idleState { get; private set; }

    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        //insert state info

        moveState = new SmallFishMoveState(this, stateMachine, this); //insert later
        idleState = new SmallFishIdleState(this, stateMachine, this);

    }
    
    protected override void Start()
    {
        base.Start();
        
        //initialize correct state
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public bool CheckMoving() //see if the current object is moving
    {
        float threshold = 0.05f;
        if (base.rb.velocity.magnitude <= threshold)
        {
            return false;
        }
        return true;
    }

    public void OnBecameInvisible()
    {
        Debug.Log("Small Fish became invisible");
        Destroy(gameObject);
    }
    
    //add force here
    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector3 force_direction = new Vector3(facingDir, 1,0);
        force_direction = force_direction * hitForce;
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(force_direction, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
