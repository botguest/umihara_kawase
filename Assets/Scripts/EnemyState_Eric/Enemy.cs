using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    GameObject thePlayer;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float shockTime; //Jingxing's Mods, for counting down the shock time after hook is gone

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    
    //Jingxing's Mods
    public bool grappled;
    //Jingxing's Mods
    
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        
        //Jingxing's Mods
        thePlayer = GameObject.FindWithTag("Player");
        thePlayer.GetComponent<GrapplingHook2>().onEnemyGrappled.AddListener(Grappled);
        grappled = false;
        //Jingxing's Mods
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update(); 
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    
    //Jingxing's Mods
    void Grappled(GameObject grappledObject)
    {
        if (grappledObject == this.gameObject)
        {
            Debug.Log("Grappling Hook Grappled " + this.gameObject);
            // it is working!
            grappled = true;
            //let the state transition begin... Access grappled.
            thePlayer.GetComponent<SpringJoint2D>().connectedBody = rb;
            //thePlayer.GetComponent<SpringJoint2D>().connectedAnchor = this.transform.position;
        }
    }
    //Jingxing's Mods
}
