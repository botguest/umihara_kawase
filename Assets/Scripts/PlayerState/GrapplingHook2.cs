using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GrapplingHook2 : MonoBehaviour
{
    private Vector3 lastDirection = Vector3.right;
    private bool isHooked = false;
    private bool isFiring = false; //grapple hook is traveling
    private Transform player_transform;
    private Vector3 hook;
    [SerializeField] private float hookSpeed;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float rope_max_dist;
    [SerializeField] private float deviation;
    [SerializeField] private LayerMask grappledLayer;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private SpringJoint2D joint;
    private float grappleLength;
    [SerializeField] private float ropeLengthenSpeed;
    private Vector3 grapplePoint;
    [SerializeField] private float tensionDistance;
    [SerializeField] private GameObject player;
    private float freeMovingDist;
    private Vector3 lastDirectionSaved;
    private GameObject hitObject;
    private bool hit;
    

    //Jingxing's Mods
    public UnityEvent<GameObject> onEnemyGrappled; //the event of when an enemy is grappled.
    //Jingxing's Mods


    //still need to implement a basic spring potential energy system, for the acceleration is not precise
    //
    private void Awake()
    {
        rope.enabled = false;
        joint.enabled = false;
        player_transform = GetComponent<Transform>();
        
        //Jingxing's Mods
        if (onEnemyGrappled == null)
            onEnemyGrappled = new UnityEvent<GameObject>(); //setting up
        //Jingxing's Mods
    }
    void Update()
    {
        //Debug.Log(EnemyDetected());
        CheckDirectionInput();
        Debug.Log("isHooked: " + isHooked + ", isFiring: " + isFiring);

        if (Input.GetKey(KeyCode.J))
        {
            if (!isHooked && !isFiring)
            {
                rope.enabled = false;
                CheckGrappleLaunch();
            }else if (!isHooked && isFiring) //firing state
            {
                Debug.Log("exec");
                FiringGrapple();
            }
            else if (isHooked && !isFiring)// hanged state
            {
                //rope shorten and lengthen logic
                if (isWallDetected())
                {
                    hookAttachedToWall();
                    Debug.Log("attached to wall!");
                }
                if (hit)
                {
                    hookAttachedToEnemy();
                    Debug.Log("attached to enemy!");
                }
                    
            }
            else
            {
                return;
            }
        }

        
        

        if(Input.GetKeyUp(KeyCode.J))
        {
            joint.connectedBody = null;
            joint.enabled = false;
            rope.enabled = false;
            isHooked = false;
            isFiring = false;
            hitObject = null;
            hit = false;
            
        }
        
        
    }

    void LaunchGrapple(Vector2 direction) //will be only called once
    {
        hook = player_transform.position; //reset the hook pos
        Debug.Log("Launch grapple in direction: " + direction);
        isFiring = true;
        isHooked = false;
        lastDirectionSaved = lastDirection;
    }

    void FiringGrapple()
    {
        Debug.Log("Firing grapple in direction: " + lastDirection);
        hook += lastDirectionSaved * Time.deltaTime * hookSpeed;
        
        DrawRope(hook);
        //only three possibilities: touches a wall, touches an enemy or reach maximum distance. Else would be release button.
        if (isWallDetected())
        {
            isFiring = false;
            isHooked = true;

            //these are put here because they should only be triggered once
            
            joint.enabled = true;
            grapplePoint = hook;
            joint.connectedAnchor = grapplePoint;
            freeMovingDist = Vector2.Distance(grapplePoint, player_transform.position);

        }

        if (isMaxDistReached())
        {
            isFiring = false;
            isHooked = false;
            Debug.Log("Maximum Distance Reached!");
        }

        if(EnemyDetected())
        {
            player.GetComponent<Player>().GrappleEnemy();
            //enemy stunned logic should be written in enemy itself?
            isFiring = false;
            isHooked = true;
            hitObject = EnemyDetected();

            //these are put here because they should only be triggered once
            joint.enabled = true;
            joint.connectedBody = hitObject.GetComponent<Rigidbody2D>();
            freeMovingDist = Vector2.Distance(hitObject.transform.position, player_transform.position);
        }
    }

    void hookAttachedToWall()
    {
        float maxDist = freeMovingDist + tensionDistance;
        grappleLength = Vector2.Distance(grapplePoint, player_transform.position);
        if (grappleLength > freeMovingDist) //if the player pulls the rope, feels tension
            joint.distance = freeMovingDist;
        else
            joint.distance = grappleLength; //if player is moving inside free moving distance, player can freely move without tension
        
        DrawRope(grapplePoint);
        
        //there are two distances. One distance is free moving distance, in which player can move freely without feeling the tension. But once this distance is exceeded, the player will feel tension, and cannot move out a fixed distance.

        if(Input.GetKey(KeyCode.S) && freeMovingDist >= .2f) //when player shortens the wire, the free moving distance and distance of spring both decreases, while frequency remains unchanged. So player will be pulled.
        {
            freeMovingDist -= ropeLengthenSpeed * Time.deltaTime;
            grappleLength -= ropeLengthenSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W) && freeMovingDist <= rope_max_dist) //when players lengthens the wire, the free moving distance increases, but the distance doesn't change. When the player moves the distance changes.
        {
            freeMovingDist += ropeLengthenSpeed * Time.deltaTime;
            grappleLength += ropeLengthenSpeed * Time.deltaTime;

        }
    }

    void hookAttachedToEnemy()
    {
        float maxDist = freeMovingDist + tensionDistance;
        grappleLength = Vector2.Distance(hitObject.transform.position, player_transform.position);
        if (grappleLength > freeMovingDist) //if the player pulls the rope, feels tension
            joint.distance = freeMovingDist;
        else
            joint.distance = grappleLength; //if player is moving inside free moving distance, player can freely move without tension

        DrawRope(hitObject.transform.position);
        //there are two distances. One distance is free moving distance, in which player can move freely without feeling the tension. But once this distance is exceeded, the player will feel tension, and cannot move out a fixed distance.

        if (Input.GetKey(KeyCode.S) && freeMovingDist >= .2f) //when player shortens the wire, the free moving distance and distance of spring both decreases, while frequency remains unchanged. So player will be pulled.
        {
            freeMovingDist -= ropeLengthenSpeed * Time.deltaTime;
            grappleLength -= ropeLengthenSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W) && freeMovingDist <= rope_max_dist) //when players lengthens the wire, the free moving distance increases, but the distance doesn't change. When the player moves the distance changes.
        {
            freeMovingDist += ropeLengthenSpeed * Time.deltaTime;
            grappleLength += ropeLengthenSpeed * Time.deltaTime;

        }
    }



    bool isWallDetected()
    {
        bool hit = Physics2D.OverlapPoint(hook, grappledLayer);
        return hit;
    }
    bool isMaxDistReached()
    {
        float dist = Vector2.Distance(hook, player_transform.position) - rope_max_dist;
        if (dist < deviation && dist > 0)
        {
            return true;
        }else
        {
            return false;
        }
    }

    GameObject EnemyDetected()
    {
        Collider2D hitObject = Physics2D.OverlapPoint(hook, enemyLayer);
        bool _hit = Physics2D.OverlapPoint(hook, enemyLayer);
        //Jingxing's Mods
        if (_hit)
        {
            hit = true;
            onEnemyGrappled.Invoke(hitObject.GameObject()); //invoking the event & passing through hit object.
        }
        //Jingxing's Mods
        if (hitObject == null) //just to make my debugger happy
        {
            return null;
        }
        return hitObject.gameObject;

    }

    void CheckDirectionInput()
    {
        // Reset direction input
        Vector2 directionInput = Vector2.zero;

        // Horizontal input
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            directionInput.x = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            directionInput.x = 1;
        }

        // Vertical input
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            directionInput.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            directionInput.y = -1;
        }

        // Update last direction only if there is some input
        if (directionInput != Vector2.zero)
        {
            lastDirection = directionInput.normalized;
        }
    }

    void CheckGrappleLaunch()
    {
            if (Input.GetKeyDown(KeyCode.J))
            {
                LaunchGrapple(lastDirection);
            }
    }

    public void DrawRope(Vector3 pos1)//draw line from player to pos1
    {
        rope.enabled = true;
        rope.SetPosition(0, player_transform.position);
        rope.SetPosition(1, pos1);
    }
}
