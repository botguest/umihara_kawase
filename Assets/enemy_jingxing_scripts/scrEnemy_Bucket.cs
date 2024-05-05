using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemy_Bucket : Enemy
{
    
    public GameObject SmallFish; //a ref to small fish. assigned in unity editor
    public float launchForce = 100f; //the force of launched fish
    
    #region States

    public BucketIdleState idleState { get; private set; }
    public BucketGrappledState grappledState { get; private set; }

    #endregion
    
    private float launchAngle; //the angle of launched fish

    protected override void Awake()
    {
        base.Awake();

        idleState = new BucketIdleState(this, stateMachine, this);
        grappledState = new BucketGrappledState(this, stateMachine, this);
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

    public void FireFishRepeatedlyStarter(bool fire_to_left)
    {
        StartCoroutine(FireFishRepeatedly(fire_to_left));
    }
    
    private IEnumerator FireFishRepeatedly(bool fire_to_left)
    {
        int count = 0;
        while (count < 3)
        {
            FireFish(fire_to_left);
            yield return new WaitForSeconds(0.5f); // Wait for one second before continuing in next frame
            count++;
        }
    }

    public void FireFish(bool to_left)
    {
        Debug.Log("Bucket firing to the left = " + to_left);

        if (to_left)
        {
            launchAngle = 120.0f; //change here later
        }
        else
        {
            launchAngle = 60.0f; //change here later
        }
        
        //need a fish object too
        //Work on this later. Works intended so far.
        float angleInRadians = launchAngle * Mathf.Deg2Rad;
        Vector2 launchDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        
        
        GameObject SmallFishSpawned = Instantiate(SmallFish, transform.position + new Vector3(0,1,0), transform.rotation);
        SmallFishSpawned.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchForce);

        if (to_left)
        {
            SmallFishSpawned.GetComponent<scrEnemy_SmallFish>().Flip();
        }
    }
    
    //need a on collisionEnter
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && IsMyStateGrappled<BucketGrappledState>())
        {
            Destroy(gameObject);
        }
    }
}