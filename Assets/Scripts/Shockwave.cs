using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/Shockwave")]
public class Shockwave : PowerUp
{
    //Script References
    SpawnAndChase reference;
    PowerUpManager manager;

    //Finds object to reference then calls tapIgnore() to have Hit() called on every object for each tap for 
    // the length of the coroutine set by the PowerUpManager in Delay().
    public override void Activate(GameObject player){
        reference = FindObjectOfType<SpawnAndChase>();
        reference.tapIgnore(true, false);
        manager = FindObjectOfType<PowerUpManager>();
        manager.Delay(this);
    }

    //When this func is called after the coroutine tapping logic will return to normal
    public override void AfterDelay(){
        reference.tapIgnore(false, false);
    }
}
