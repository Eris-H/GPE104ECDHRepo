using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedPowerup : Powerup
{
    public float newSpeed;
    private float oldSpeed;

    public override void Apply(PowerupManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();

        oldSpeed = tankPawn.moveSpeed;

        if (tankPawn != null)
        {
            tankPawn.moveSpeed = newSpeed;
            
        }
    }

    public override void Remove(PowerupManager target)
    {
        TankPawn tankPawn = target.GetComponent<TankPawn>();

        if (tankPawn != null)
        {
            tankPawn.moveSpeed = oldSpeed;
        }
    }
}
