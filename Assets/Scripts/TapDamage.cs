using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDamage : MonoBehaviour
{
    //Variables
    public static TapDamage Damager { get; private set; } //Set it as a Singleton
    private int tapDamage = 1;

    private void Awake()
    {
        if (Damager == null)
        {
            Damager = this;
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates
        }
    }
    
    //Increases touch damage by 1
    public void IncreaseDmg(){
        tapDamage++;
    }

    public int getDmg(){
        return tapDamage;
    }
}
