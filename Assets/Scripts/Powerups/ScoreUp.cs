using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreUp : Powerup
{

    public int ScoreToAdd; 

    public override void Apply(PowerupManager target)
    {
        Pawn pawn = target.GetComponent<Pawn>();

        if (pawn != null)
        {
            Controller controller = target.GetComponent<Pawn>().controller;

            if (controller != null)
            {
                controller.AddToScore(ScoreToAdd);
                Debug.Log(pawn.controller.score);
            }        
        }

    }

    public override void Remove(PowerupManager target)
    {

    }

}
