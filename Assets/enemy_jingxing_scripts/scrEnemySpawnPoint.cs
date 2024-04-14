using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//This is the script for enemy spawn points. This object handles spawning an enemy prefab.
//a flag for outside manager. If flag == true, respawn an enemy

public class scrEnemySpawnPoint : MonoBehaviour
{

    public bool spawn; //the flag, controlled by manager
    public int likeliness; //how likely for this spawn point to spawn. 0-100
    
    public GameObject enemy_0; //to be spawned
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        likeliness = 5; //instantiating likeliness
        StartCoroutine(TrySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnChecker();
    }
    
    
    
    //Spawn_Checker, put this in Update()
    void SpawnChecker()
    {
        if (spawn)
        {
            Spawn();
        }
    }

    //When called, spawn an enemy
    bool Spawn()
    {
        Instantiate(enemy_0, transform.position, Quaternion.identity);
        return true;
    }

    //a coroutine is a method con return type IEnumerator & a yield return statement included in the body
    private IEnumerator TrySpawn()
    {
        
        likeliness = Mathf.Clamp(likeliness, 0, 100);
        System.Random random = new System.Random();
        
        while (true)
        {
            
            
            yield return new WaitForSeconds(1f); //the point of execution pause & resume in following frame (after 1s)
        }
    }
    
}
