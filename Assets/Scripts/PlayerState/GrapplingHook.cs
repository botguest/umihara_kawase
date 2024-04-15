using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Grapple Info")]
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private float grappleSpd;
    [SerializeField] private float ropeMaxLength;

    private Player player;
    private Vector3 grapplePoint;
    private SpringJoint2D joint;
    private bool startReturn = false;
    private Vector3 preAnchorPoint;
    private bool canLaunch = true;
    private Vector3 returnPoint;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
        rope = gameObject.GetComponentInChildren<LineRenderer>();
        rope.enabled = true;
        player = GetComponent<Player>();
        preAnchorPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canLaunch);
        Debug.Log("rope" + rope.enabled);
        if(canLaunch)
        {
            if(Input.GetKey(KeyCode.J))
            {
                rope.enabled = true;
                if (Input.GetKey(KeyCode.W))
                {
                    Launch(Vector2.up, ropeMaxLength);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Launch(Vector2.down, ropeMaxLength);
                }
                else
                {
                    if(player.facingDir == 1)
                    {
                        Launch(Vector2.right, ropeMaxLength);
                    }
                    else
                    {
                        Launch(Vector2.left, ropeMaxLength);
                    }
                }
            }else
            {
                joint.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.J))
            {
                returnPoint = preAnchorPoint;
                startReturn = true;
                canLaunch = false;
            }
        }

        

        if(startReturn)
        {
            hookReturn();
        }
    }

    private void Launch(Vector3 launch_dir, float max_launch_dist)
    {
        DrawRope(preAnchorPoint);
        Vector3 max_pos = launch_dir * max_launch_dist; // the maximum distance and direction the hook can go to
        bool preAnchorPoint_Going = true; //if the pre anchor point is going towards the max_pos
        bool preAnchorPoint_ReachMaxDist = false; //if pre anchor point reaches max_pos

        if (preAnchorPoint_Going)
            preAnchorPoint += launch_dir * grappleSpd * Time.deltaTime; //move preAnchorPoint to max position, if it didn't touch the wall yet
        else//the anchor point is now attached to wall
        {
            joint.distance = grappleLength;
        }


        if (hookDetectWall(preAnchorPoint)) //if the preAnchorPoint touches a wall, set it to anchorpoint and enable joint. This should only be called once
        {
            Debug.Log("hit");
            joint.enabled = true; //enable joint
            joint.connectedAnchor = preAnchorPoint; //set anchor to pre anchor pos
            DrawRope(joint.connectedAnchor);
            preAnchorPoint_Going = false; //stop the pre anchor point from going further
            preAnchorPoint = player.transform.position; //reset pre anchor point to default
        }
        

        //if the pre anchor point reaches max_pos
        if(preAnchorPoint == max_pos)
            preAnchorPoint_ReachMaxDist = true;

        if (preAnchorPoint_ReachMaxDist)
            hookReturn();
    }

    private bool hookDetectWall(Vector3 hook_pos) //detects if the hook touches a can-be-grappled wall
    {
        bool hit = Physics2D.OverlapPoint(hook_pos, grappleLayer);
        return hit;
    }

    private void hookReturn()
    {
        DrawRope(returnPoint);
        preAnchorPoint = player.transform.position;
        returnPoint += (player.transform.position - returnPoint).normalized * grappleSpd * Time.deltaTime;

        if(Vector2.Distance(returnPoint, player.transform.position) <= .5f)
        {
            startReturn = false;
            rope.enabled = false;
            canLaunch = true;
        }
    }

    private void DrawRope(Vector3 pos1)//draw line from player to pos1
    {
        rope.SetPosition(0, player.transform.position);
        rope.SetPosition(1, pos1);
    }

}
