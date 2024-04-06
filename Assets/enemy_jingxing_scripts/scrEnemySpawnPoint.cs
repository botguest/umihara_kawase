using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script for enemy spawn points. This object handles spawning an enemy prefab.
//a flag for outside manager. If flag == true, respawn an enemy

public class scrEnemySpawnPoint : MonoBehaviour
{

    public bool spawn; //the flag, controlled by manager
    public GameObject enemy_0;
    
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
    
    //Spawn_Checker

    //When called, spawn an enemy
    bool Spawn()
    {
        Instantiate(enemy_0, transform.position, Quaternion.identity);
        return true;
    }
}
