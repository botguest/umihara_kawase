using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsLadder;
    [SerializeField] protected LayerMask whatIsCertainMask;
    [SerializeField] protected float ladderDetectRadius;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;


    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }

    #region collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public virtual bool isUpLadderDetected() => Physics2D.OverlapCircle(this.transform.position, ladderDetectRadius, whatIsLadder);

    public virtual bool isDownLadderDetected() => Physics2D.OverlapCircle(this.transform.position - new Vector3(0,.5f,0), ladderDetectRadius, whatIsLadder);
    public virtual bool isCertainMaskDetected() => Physics2D.OverlapCircle(this.transform.position, ladderDetectRadius, whatIsCertainMask);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawSphere(this.transform.position, ladderDetectRadius);
        Gizmos.DrawSphere(this.transform.position - new Vector3(0, .5f, 0), ladderDetectRadius);
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    #endregion

    #region Velocity
    public void SetZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    public void Addforce(float _xVelocity, float _yVelocity, float spd)
    {
        Vector2 movement = new Vector2(_xVelocity, _yVelocity);
        rb.AddForce(movement * spd);
        FlipController(rb.velocity.x);
    }

}
