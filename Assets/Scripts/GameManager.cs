using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    [Header ("Script References")]
    public GameOverHUD OverHUD;
    public Score fscore;
    public SpawnAndChase obstacles;
    public PowerUpManager pumRef;

    private static float playTime = 0;

    //Pause Function Booleans
    private static bool spawnPause = false;
    private static bool chasePause = false;
    private static bool timePause = false;
    private bool gameEnded = false;

    //Levels
    private int obstacleMaxLevel = 1;
    private int powerupLevel = 1;

    //Updates playTime and runs functions Chase and Increase obstacle and power-up functions
    void Update()
    {
        if (!timePause && !gameEnded)
            playTime += Time.deltaTime;
        obstacles.removeNull();
        if (!chasePause)
            obstacles.Chase();
        ObstacleIncrease();
        PowerUpIncrease();
    }

    //Runs Spawning function every physics update
    void FixedUpdate()
    {
        if (!spawnPause && !gameEnded)
            obstacles.SpawnTester();
    }

    //Increases max obstacles every x seconds passed
    private void ObstacleIncrease()
    {
        if (playTime > obstacleMaxLevel * 150)
        {
            obstacleMaxLevel++;
            obstacles.UpdateMax();
        }
    }

    //Gives player a new power-up every x seconds passed
    private void PowerUpIncrease()
    {
        if (playTime > powerupLevel * 20)
        {
            pumRef.runPowerUp();
            powerupLevel++;
        }
    }

    // Stops and deactivates score and obstacle spawn. Sets gameEnded to true and sets up GameOver screen. And Saves fragment data 
    public void GameOver()
    {
        gameEnded = true;
        fscore.stopScore();
        SaveAndLoad.SAL.SaveData();
        OverHUD.Setup(fscore.getScore());
        obstacles.Clear();
        Destroy(this.gameObject.GetComponent<PowerUpManager>());
        Destroy(gameObject);
    }
    
    //Stops Spawn and/or Chase actions for obstacles
    public void StopActions(bool Spawn, bool Chase)
    {
        spawnPause = Spawn;
        chasePause = Chase;
    }

    //Pauses the playTime 
    public void pauseTime(bool timeUpdate)
    {
        timePause = timeUpdate;
    }

    public float getPlayedTime()
    {
        return playTime;
    }
    
}
