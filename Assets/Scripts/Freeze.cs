using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Freeze")]
public class Freeze : PowerUp
{
    //Script References
    SpawnAndChase reference;
    PowerUpManager manager;

    //Finds a reference to SpawnAndChase and then calls StopActions() to stop the Chase() and Spawn() function from performing 
    //then runs the coroutine set in Delay() by the manager reference of PowerUpManager()
    public override void Activate(GameObject player){
        
        reference = FindObjectOfType<SpawnAndChase>();
        reference.StopActions(true, true, false);
        manager = FindObjectOfType<PowerUpManager>();
        manager.Delay(this);
    }

    //Runs when the coroutine in PowerUpManager() is done resuming all gameplay actions
    public override void AfterDelay(){
        reference.StopActions(false, false, false);
    }
}
