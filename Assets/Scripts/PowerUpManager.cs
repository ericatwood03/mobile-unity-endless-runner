using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour //Monobehaviour allows you to define functionality of scripts through componenets and enables use of code based on events
{
    //Variables

    public Score scoreRef;
    public SpawnAndChase scRef;
    public ScrollingBG scrollRef;
    public List<PowerUp> powerUps = new List<PowerUp>();
    public PowerUpUI powerUpUI;
    public GameObject player;
    public ParticleSystem starTrail;
    
    private PowerUp powerUp1;
    private PowerUp powerUp2;
    private float playTime;
    private float checkTime = 20;
    private float checkUp = 20;
    private float waitSeconds = 3f;
    private int p1;
    private int p2;

    // Update is called once per frame
    // Checks every time playTime is >= checkTime then checkTime is increased and all gameplay actions are paused using toggleGameplay().
    // Randomize() is run to set powerUp options then showChoices() is run
    void Update()
    {
        playTime = scRef.getPlayedTime();
        if(playTime > checkTime){
            ToggleGameplay(true);
            checkTime += checkUp;
            Randomize();
            showChoices();
        }
    }

    // Pauses/Unpause all aspects of gameplay for UI choices
    private void ToggleGameplay(bool toggle){
        scoreRef.StopTime(toggle);
        scRef.StopActions(toggle, toggle, toggle);
        scrollRef.stopScroll(toggle);
        scRef.tapIgnore(toggle, toggle);
        if(toggle)
            starTrail.Pause();
        else{
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
