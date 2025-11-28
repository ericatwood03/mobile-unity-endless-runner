using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables
    [Header ("Script References")]
    public GameOverHUD OverHUD;
    public Score fscore;
    public SpawnAndChase obstacles;
    public PowerUpManager pumRef;

    public GameObject pauseMenu;
    
    private static float playTime = 0;

    //Pause Function Booleans
    private static bool spawnPause = false;
    private static bool chasePause = false;
    private static bool timePause = false;
    private bool gameEnded = false;
    private bool parameterOne;
    private bool parameterTwo;

    //Levels
    private int obstacleMaxLevel = 1;
    private int powerupLevel = 1;

    //Updates playTime and runs functions Chase and Increase obstacle and power-up functions
    void Start()
    {
        Input.backButtonLeavesApp = true;
    }

    //Calls Chase and an increase to Obstacle max count and new PowerUp
    void Update()
    {
        if (!timePause && !gameEnded)
            playTime += Time.deltaTime;
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

    //On Back button, Screen lock, and Other game exiting actions
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Pause();
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    //Sets timescale to 0, remembers state of game before pause, and activates pause menu
    public void Pause()
    {
        bool parameterOne = obstacles.getIgnore();
        bool parameterTwo = obstacles.getPause();
        obstacles.tapIgnore(true, true);
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    // Resumes gameplay and sets timeScale back to 1
    public void Resume()
    {
        Time.timeScale = 1f;
        obstacles.tapIgnore(parameterOne, parameterTwo);
        pauseMenu.SetActive(false);
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
        obstacles.Clear();
        gameEnded = true;
        playTime = 0;
        fscore.stopScore();
        SaveAndLoad.SAL.SaveData();
        OverHUD.Setup(fscore.getScore());
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
