using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour //Monobehaviour allows you to define functionality of scripts through componenets and enables use of code based on events
{
    //Variables
    [Header ("Script References")]
    public Score scoreRef;
    public SpawnAndChase scRef;
    public GameManager manager;
    public ScrollingBG scrollRef;
    private GameObject fake;

    public List<PowerUp> powerUps = new List<PowerUp>();
    public PowerUpUI powerUpUI;
    public GameObject player;
    public ParticleSystem starTrail;
    
    [Header ("Power Ups")]
    private PowerUp powerUp1;
    private PowerUp powerUp2;

    [Header ("Time Checks")]
    private float waitSeconds = 3f;

    [Header ("Randomizer Ints")]
    private int p1;
    private int p2;

    //All gameplay actions are paused using toggleGameplay().
    //Randomize() is run to set powerUp options then showChoices() is run
    public void runPowerUp()
    {
        ToggleGameplay(true);
        Randomize();
        showChoices();
    }

    // Pauses/Unpause all aspects of gameplay for UI choices
    private void ToggleGameplay(bool toggle)
    {
        scoreRef.StopTime(toggle);
        manager.StopActions(toggle, toggle);
        manager.pauseTime(toggle);
        scrollRef.stopScroll(toggle);
        scRef.tapIgnore(toggle, toggle);
        if (toggle)
            starTrail.Pause();
        else
        {
            starTrail.Play();
        }
    }

    //Randomly chooses 2 powerups ensuring they are not the same then puts them into the powerUp1 and 2 variables
    private void Randomize(){
        p1 = Random.Range(0,powerUps.Count);
        p2 = Random.Range(0, powerUps.Count);
        while(p2 == p1){
            p2 = Random.Range(0, powerUps.Count);
        }
        powerUp1 = powerUps[p1];
        powerUp2 = powerUps[p2];
    }
    
    //Calls the Show() method in the UI script
    private void showChoices(){
        powerUpUI.Show(powerUp1, powerUp2, PreActivate);
    }

    //Calls Activate() on whatever powerup is chosen
    private void PreActivate(PowerUp pu){
        ToggleGameplay(false);
        pu.Activate(player);
    }

    //Starts a coroutine with an amount of time to take and a powerUp to return to
    public void Delay(PowerUp pu){
        StartCoroutine(WaitFor(waitSeconds, pu));
    }

    //Waits for set amount of time then runs AfterDelay() on the powerUp given
    IEnumerator WaitFor(float duration, PowerUp pu){ //IEnumerator is a state machine, Unity calls MoveNext() on it every frame
        yield return new WaitForSeconds(duration); // pauses execution of the coroutine for the duration of time
        pu.AfterDelay();
    }
}
