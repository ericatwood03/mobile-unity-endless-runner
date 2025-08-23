using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUp/DamageUp")]
public class DamageUp : PowerUp
{

    //calls IncreaseDmg() from TapDamage
    public override void Activate(GameObject player){
        TapDamage.Damager.IncreaseDmg();
    }

    //Does nothing as the function is not needed for this powerup
    public override void AfterDelay()
    {
        return;
    }
}
