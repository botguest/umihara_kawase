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
    public float distanceToSpawnMore; //the distance shorter than which the spawn point is not going to spawn.
    public float distanceToWayTooClose;
    public float spawnCounterOriginalVal;
    
    public Enemy enemy_0; //to be spawned
    
    
    private GameObject thePlayer;
    
    //the tooClose #1, if too close more likely to spawn
    private bool tooClose = false;
    private bool prevClose = false;

    private bool wayTooClose = false;
    private bool prevWayTooClose = false;

    private float spawnCounter;

    #region Start & Update
    
    // Start is called before the first frame update
    void Start()
    {
        likeliness = 10; //instantiating likeliness
        spawnCounter = spawnCounterOriginalVal;
        SetPlayer();
        StartCoroutine(TrySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCounterTick();
        IsTooCloseFlipped(30); //if yes, alter likeliness accordingly
        IsWayTooCloseFlipped(100);
        SpawnChecker();
    }
    
    #endregion

    
    #region methods
    
    //Spawn_Checker, put this in Update()
    void SpawnChecker()
    {
        if (spawn && spawnCounter <= 0)
        {
            Spawn();
            spawnCounter = spawnCounterOriginalVal;
        }
    }

    bool SetPlayer() //put in start
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            thePlayer = GameObject.FindWithTag("Player");
            return true;
        }

        return false;
    }

    bool IsTooCloseFlipped(int likeliness_magnitude) //if yes, alter likeliness accordingly
    {
        CheckIfTooClose(); //integrated
        
        if (prevClose != tooClose) //if it has been flipped
        {
            if (prevClose == true) //close to far
            {
                likeliness = likeliness - likeliness_magnitude;
            }
            
            else if (prevClose == false) //far to close
            {
                likeliness = likeliness + likeliness_magnitude;
            }
            
            prevClose = tooClose;
            return true;
        }

        return false;

    }

    bool IsWayTooCloseFlipped(int likeliness_magnitude)
    {
        CheckIfWayTooClose();
        
        if (prevWayTooClose != wayTooClose) //if it has been flipped
        {
            if (prevWayTooClose == true) //close to far
            {
                likeliness = likeliness + likeliness_magnitude;
            }
            
            else if (prevWayTooClose == false) //far to close
            {
                likeliness = likeliness - likeliness_magnitude;
            }
            
            prevWayTooClose = wayTooClose;
            return true;
        }

        return false;
    }

    void SpawnCounterTick()
    {
        spawnCounter = spawnCounter - Time.deltaTime;
    }
    
    #endregion
    
    #region helpers
    //When called, spawn an enemy
    bool Spawn()
    {
        //debugging
        print("spawned!");
        
        Instantiate(enemy_0, transform.position, Quaternion.identity);
        spawn = false;
        return true;
    }
    
    float DistanceToPlayer() //helper
    {
        Vector3 direction = this.transform.position - thePlayer.transform.position;
        float distance = direction.magnitude;
        
        return distance;
    }
    
    bool CheckIfTooClose()
    {
        if (DistanceToPlayer() <= distanceToSpawnMore)
        {
            tooClose = true;
            return true;
        }
        else
        {
            tooClose = false;
            return false;
        }
    }

    bool CheckIfWayTooClose()
    {
        if (DistanceToPlayer() <= distanceToWayTooClose)
        {
            wayTooClose = true;
            return true;
        }
        else
        {
            wayTooClose = false;
            return false;
        }
    }
    #endregion
    
    #region Coroutines
    //a coroutine is a method con return type IEnumerator & a yield return statement included in the body
    private IEnumerator TrySpawn()
    {
        
        likeliness = Mathf.Clamp(likeliness, 0, 100);
        System.Random random = new System.Random();
        
        while (true)
        {
            //debugging
            Debug.Log("spawn likeliness: " + likeliness);
            
            //call spawn maybe?
            if (random.Next(0, 100) <= likeliness)
            {
                spawn = true;
            }
            
            yield return new WaitForSeconds(2f); //the point of execution pause & resume in following frame (after 1s)
        }
    }
    
    #endregion
    
}
