using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndChase : MonoBehaviour
{
    //Static Variables
    public static float playTime = 0;
    private static bool delay = true;

    //Variables
    public GameObject[] spawnPoints;
    public GameObject[] prefabs;
    public GameObject player;
    
    private List<GameObject> obstacles = new List<GameObject>();
    private int limit = 3;
    private int maxLimit = 20;
    private float point = 0;
    private bool gameEnded = false;
    private int dupl = 0;
    private static bool spawnPause = false;
    private static bool chasePause = false;
    private static bool timePause = false;
    private static bool shouldIgnore = false;
    private static bool pausing = false;

    //Calls the WaitFor function for 2 seconds
    void Start()
    {
        StartCoroutine(WaitFor(2)); //Coroutines still run on the main thread
    }

    //Updates playTime variable, Cleans out the obstacles list, and then runs Chase()
    void Update()
    {
        if(!timePause && !gameEnded)
            playTime += Time.deltaTime;
        obstacles.RemoveAll(item => item == null);
        if(!chasePause)
            Chase();
    }
    
    
    void FixedUpdate()
    {
        UpdateMax();
        SpawnTester();
    }

    // Checks for every 200 seconds since game start up then 
    // increases the limit of obstacles on the screen at once as long as the limit is below 8
    private void UpdateMax(){
        if(playTime > point + 150  && limit < maxLimit){
            point = playTime;
            limit++;
        }
    }

    // Checks if there are less obstacles then limit and if delay is false. 
    // If true then increments dupl by 1, if dupl is less then 2 and the game hasn't ended and the Spawn() action isnt paused 
    // run SpawnChance and decrement dupl
    private void SpawnTester(){
        if(obstacles.Count <= limit && !delay){
            dupl++;
            if(dupl < 2 && !gameEnded && !spawnPause){ //dupl prevents two obstacles from being spawned at the same time and gameEnded stops spawning once game is over
                SpawnChance();
            }
            dupl --;
        }
    }

    //Randomly chances running the Spawn() function based on how many obstacles already exist
    private void SpawnChance(){
        var rand = Random.Range(0,150);
        var x = 8;
        // Sets x by how many obstacles exist
        if(obstacles.Count > 3){
            x -= obstacles.Count - 3;
        }
        //If random num is less than or equal to x call Spawn()
        if(rand <=x){
            Spawn();
        }
    }

    //  Calculates the rarity of each obstacle spawn based off of the time since the start of the game. 
    //  And then uses that rarity to calculate which obstacle should spawn
    GameObject Rarity(){
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
    private void Chase(){
        for(int i = 0; i < obstacles.Count; i++){
            obstacles[i].transform.position = Vector3.MoveTowards(obstacles[i].transform.position, player.transform.position, 1f * Time.deltaTime);
        }
    }

    //Spawns a random obstacle at a random spawn location then add it to the obstacles list
    private void Spawn(){
        var sr = Random.Range(0, spawnPoints.Length);
        GameObject randobj = Instantiate(Rarity(), spawnPoints[sr].transform.position, Quaternion.identity);
        randobj.transform.position = new Vector3(randobj.transform.position.x, randobj.transform.position.y, 1); //Modifies the z position to be on screen
        obstacles.Add(randobj);
    }

    //Waits for the # provided by duration then sets delay to false
    IEnumerator WaitFor(float duration){
        yield return new WaitForSeconds(duration);
        delay = false;
    }

    //Stops Spawn() from running and then destroys each object in the obstacles list before clearing it
    public void Clear(){
        gameEnded = true;
        for(int i = 0; i < obstacles.Count; i++)
            Destroy(obstacles[i].gameObject);
        obstacles.Clear();
        playTime = 0;
    }

    // Pauses/Unpauses Spawn(), Chance(), and time updating
    public void StopActions(bool Spawn, bool Chase, bool timeUpdate){
        spawnPause = Spawn;
        chasePause = Chase;
        timePause = timeUpdate;
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
    
    public float getPlayedTime(){
        return playTime;
    }
}