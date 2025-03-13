using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes the variables visible
[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;

    public override void Apply(PowerupManager target)
    {
        Health health = target.GetComponent<Health>();

        if (health != null)
        {
            health.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        Health health = target.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(healthToAdd, target.GetComponent<Pawn>());
        }
    }
}
