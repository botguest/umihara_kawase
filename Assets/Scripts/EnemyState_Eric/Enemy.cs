using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        
        //Jingxing's Mods
        GameObject thePlayer = GameObject.FindWithTag("Player");
        
        thePlayer.GetComponent<GrapplingHook2>().onEnemyGrappled.AddListener(Grappled);
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
        }
    }
    //Jingxing's Mods
}
