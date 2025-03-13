using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireratePowerup : Powerup
{
    public float newFirerate;

    public override void Apply(PowerupManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();

        if (tankPawn != null)
        {
            //tankPawn.fireRate = newFirerate;
            tankPawn.UpdateFirerate(newFirerate);
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();

        if (tankPawn != null)
        {
            tankPawn.UpdateFirerate(newFirerate);
        }
    }
}
