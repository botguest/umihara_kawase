using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrShield : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player position from shield: " + GameObject.FindGameObjectWithTag("Player").transform.position);
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    
    
}
