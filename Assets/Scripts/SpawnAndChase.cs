using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndChase : MonoBehaviour
{
    //Static Variables
    public static float playTime = 0;
    private static bool delay = true;

    //GameObjects
    [Header ("Game Objects")]
    public GameObject[] spawnPoints;
    public GameObject[] prefabs;
    public GameObject player;
    
    private List<GameObject> activeObstacles = new List<GameObject>();
    
    //Integers
    private int limit = 3;
    private int maxLimit = 20;
    private int dupl = 0;

    //Booleans
    private static bool shouldIgnore = false;
    private static bool pausing = false;

    //Scripts
    [Header("Scripts")]
    public ObstaclePooler pooler;


    //Calls the WaitFor function for 2 seconds
    void Start()
    {
        StartCoroutine(WaitFor(2)); //Coroutines still run on the main thread
    }

    //Increases the limit of obstacles on the screen at once as long as the limit is below 8
    public void UpdateMax()
    {
        if (limit < maxLimit)
        {
            limit++;
        }
    }

    // Checks if there are less obstacles then limit and if delay is false. 
    // If true then increments dupl by 1, if dupl is less then 2 then runs Spawn function and decrement dupl 
    public void SpawnTester()
    {
        if (activeObstacles.Count <= limit && !delay)
        {
            dupl++;
            //dupl prevents two obstacles from being spawned 
            // at the same time and gameEnded stops spawning once game is over
            if (dupl < 2)
            {
                SpawnChance();
            }
            dupl--;
        }
    }

    //Randomly chances running the Spawn() function based on how many obstacles already exist
    private void SpawnChance(){
        var rand = Random.Range(0,150);
        var x = 8;
        // Sets x by how many obstacles exist
        if(activeObstacles.Count > 3){
            x -= activeObstacles.Count - 3;
        }
        //If random num is less than or equal to x call Spawn()
        if(rand <=x){
            Spawn();
        }
    }

    //  Calculates the rarity of each obstacle spawn based off of the time since the start of the game. 
    //  And then uses that rarity to calculate which obstacle should spawn
    private GameObject Rarity(){
        int a, b, c, d, e;
        var rand = Random.Range(0,9);
        if(playTime < 50){
            a= 5;
            b = 8;
            c = 9;
            d = 100;
            e = 100;
        }
        else if(playTime < 100){
            a = 4;
            b = 7;
            c = 8;
            d = 9;
            e = 100;
        }
        else if(playTime < 300){
            a = 4;
            b = 6;
            c = 7;
            d = 8;
            e = 9;
        }
        else{
            a = 3;
            b = 5;
            c = 6;
            d = 7;
            e = 8;
        }
        if(rand < a){
            return prefabs[Random.Range(0,4)];
        }
        else if(rand < b){
            return prefabs[Random.Range(4,8)];
        }
        else if(rand == b){
            return prefabs[Random.Range(8,10)];
        }
        else if(rand == c){
            return prefabs[Random.Range(10,11)];
        }
        else if(rand == d){
            return prefabs[Random.Range(12,16)];
        }
        else if(rand == e){
            return prefabs[Random.Range(17,18)];
        }
        else{
            return prefabs[19];
        }
    }

    //Moves all obstacles toward the Player(Star)
    public void Chase(){
        for(int i = 0; i < activeObstacles.Count; i++){
            activeObstacles[i].transform.position = Vector3.MoveTowards(activeObstacles[i].transform.position, player.transform.position, 1f * Time.deltaTime);
        }
    }

    //Passes a random prefab and spawn location to SpawnObstacle()
    private void Spawn()
    {
        var sr = Random.Range(0, spawnPoints.Length);
        pooler.SpawnObstacle(Rarity(), spawnPoints[sr].transform.position);

    }

    //Waits for the # provided by duration then sets delay to false
    IEnumerator WaitFor(float duration){
        yield return new WaitForSeconds(duration);
        delay = false;
    }

    //Calls the Release method in ObjectPooler()
    public void CallRelease(GameObject obstacle)
    {
        pooler.DespawnObstacle(obstacle);
        DeleteActive(obstacle);
    }

    //Adds active obstacles to the list
    public void AddActive(GameObject active)
    {
        activeObstacles.Add(active);
    }

    //Removes active obstacles from the list
    public void DeleteActive(GameObject active)
    {
        activeObstacles.Remove(active);
    }

    //Stops Spawn() from running and then destroys each object in the obstacles list before clearing it
    public void Clear()
    {
        for (int i = 0; i < activeObstacles.Count; i++)
        {
            pooler.DespawnObstacle(activeObstacles[i]);
        }
        activeObstacles.Clear();
        playTime = 0;
    }

    //Sets shouldIgnore and pausing
    public void tapIgnore(bool shockwaving, bool pause){
        shouldIgnore = shockwaving;
        pausing = pause;
    }

    public bool getIgnore(){
        return shouldIgnore;
    }

    public bool getPause(){
        return pausing;
    }
}