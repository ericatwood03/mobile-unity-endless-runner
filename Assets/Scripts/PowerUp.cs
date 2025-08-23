using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//Scriptable Objects are data containers, they allow you to store data independent of game objects
//Abstract classes can not be used to create objects but can only be inherited from
public abstract class PowerUp : ScriptableObject
{
    //Variables
    public string powerUpName;
    public string description;
    
    //Abstract methods can't have instructions inside it
    //Abstract methods are overwritten by classes that inherit this class
    public abstract void Activate(GameObject player);
    public abstract void AfterDelay();
}
