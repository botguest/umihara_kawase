using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script for enemy spawn points. This object handles spawning an enemy prefab.
//a flag for outside manager. If flag == true, respawn an enemy

public class scrEnemySpawnPoint : MonoBehaviour
{

    public bool spawn; //the flag
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            Spawn();
        }
    }

    bool Spawn()
    {
        
        return true;
    }
}
