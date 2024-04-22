using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Haven't Setup. Work on later. No states defined.
public class scrEnemy_SmallFish : Enemy
{
    
    
    #region States
    
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        //insert state info
    }
    
    protected override void Start()
    {
        base.Start();
        
        //initialize correct state
        //stateMachine.Initialize(moveState);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
