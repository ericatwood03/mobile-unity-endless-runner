using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Freeze")]
public class Freeze : PowerUp
{
    //Script References
    GameManager gmRef;
    PowerUpManager manager;

    //Finds a reference to GameManager and then calls StopActions() to stop the Chase() and Spawn() function from performing 
    //then runs the coroutine set in Delay() by the manager reference of PowerUpManager()
    public override void Activate(GameObject player){
        
        gmRef = FindObjectOfType<GameManager>();
        gmRef.StopActions(true, true);
        manager = FindObjectOfType<PowerUpManager>();
        manager.Delay(this);
    }

    //Runs when the coroutine in PowerUpManager() is done resuming all gameplay actions
    public override void AfterDelay(){
        gmRef.StopActions(false, false);
    }
}
