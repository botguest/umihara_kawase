using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemy_UnagiBaby : Enemy
{
    
    [Header("Unagi-specific Info")] public int initialFacingDir;
    
    #region States
    
    public UnagiBabyMoveState moveState { get; private set; }
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        moveState = new UnagiBabyMoveState(this, stateMachine, this);

        if (initialFacingDir != base.facingDir)
        {
            //flip it back, let initialFacingDir do its job
            base.Flip();
        }
    }
    
    protected override void Start()
    {
        base.Start();
        
        if (initialFacingDir != base.facingDir)
        {
            //flip it back, let initialFacingDir do its job
            base.Flip();
        }
        
        stateMachine.Initialize(moveState);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //destroy the game object after out of camera
    private void OnBecameInvisible()
    {
        Debug.Log("Unagi Baby became invisible");
        Destroy(gameObject);
    }

    public void DestroyShortCut()
    {
        Debug.Log("Destroy Shortcut Enabled");
        Destroy(gameObject);
    }
}
