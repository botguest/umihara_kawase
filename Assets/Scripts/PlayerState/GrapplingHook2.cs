using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GrapplingHook2 : MonoBehaviour
{
    private Vector2 lastDirection = Vector2.right;
    private bool isHooked = false;
    private bool isFiring = false; //grapple hook is travelling
    private Transform player_transform;
    private Vector2 hook;
    [SerializeField] private float hookSpeed;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float rope_max_dist;
    [SerializeField] private float deviation;
    [SerializeField] private LayerMask grappledLayer;
    [SerializeField] private SpringJoint2D joint;
    [SerializeField] private float grappleLength;
    [SerializeField] private float grappleLengthMax;
    [SerializeField] private float ropeLengthenSpeed;
    private Vector2 grapplePoint;


    //still need to implement a basic spring potential energy system, for the acceleration is not precise
    //
    private void Awake()
    {
        rope.enabled = false;
        joint.enabled = false;
        player_transform = GetComponent<Transform>();
    }
    void Update()
    {
        CheckDirectionInput();
        Debug.Log("isHooked: " + isHooked + ", isFiring: " + isFiring);

        if (Input.GetKey(KeyCode.J))
        {
            if (!isHooked && !isFiring)
            {
                rope.enabled = false;
                CheckGrappleLaunch();
            }
            else if (!isHooked && isFiring) //firing state
            {
                Debug.Log("exec");
                FiringGrapple();
            }
            else if (isHooked && !isFiring)// hanged state
            {
                //rope shorten and lengthen logic
                hookAttached();
            }
            else
            {
                return;
            }
        }

        if(Input.GetKeyUp(KeyCode.J))
        {
            joint.enabled = false;
            rope.enabled = false;
            isHooked = false;
            isFiring = false;
        }
        
        
    }

    void LaunchGrapple(Vector2 direction) //will be only called once
    {
        hook = player_transform.position; //reset the hook pos
        Debug.Log("Launch grapple in direction: " + direction);
        isFiring = true;
        isHooked = false;
    }

    void FiringGrapple()
    {
        Debug.Log("Firing grapple in direction: " + lastDirection);
        hook += lastDirection * Time.deltaTime * hookSpeed;
        grappleLength = Vector2.Distance(hook, player_transform.position);
        DrawRope(hook);
        //only three possibilities: touches a wall, touches an enemy or reach maximum distance. Else would be release button.
        if (isWallDetected())
        {
            isFiring = false;
            isHooked = true;
        }

        //if(isEnemyDetected())

        if (isMaxDistReached())
        {
            isFiring = false;
            isHooked = false;
            Debug.Log("Maximum Distance Reached!");
        }
    }

    void hookAttached()
    {
        grapplePoint = hook;
        joint.connectedAnchor = grapplePoint;
        joint.enabled = true;
        joint.distance = grappleLength;
        DrawRope(grapplePoint);

        if(Input.GetKey(KeyCode.W) && grappleLength < 5f)
        {
            grappleLength += ropeLengthenSpeed;
        }else if (Input.GetKey(KeyCode.S) && grappleLength > 1f)
        {
            grappleLength -= ropeLengthenSpeed;
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

    private void DrawRope(Vector3 pos1)//draw line from player to pos1
    {
        rope.enabled = true;
        rope.SetPosition(0, player_transform.position);
        rope.SetPosition(1, pos1);
    }
}
