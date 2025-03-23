using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrackTurrets : AIController
{

    //will cycle around their track
    //if they hear the player they'll attack them and will chase them
    //if player gets too far, they'll go back to patrolling
    //will always stand still to shoot

    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Patrol);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

     public override void ProcessInputs()
    {
        switch (currentState)
        {
            //each case should be one of the options in the enum
            case AIState.Patrol:
                //find target
                if (IsHasTarget())
                {
                    //do thing
                    DoPatrolState();
                }
                else
                {
                    //TargetPlayerOne();
                    TargetNearestPlayer();

                }
                //check for transition                
                //listen
                if (IsCanHear(target))
                {
                    ChangeState(AIState.Watching);
                }
            break;
            case AIState.Watching:
                //do thing
                DoWatchState();

                //check for transition
                if(!IsDistanceLessThan(target, targetDistance))
                {
                    pawn.moveSpeed = pawn.moveSpeed / 3;
                    ChangeState(AIState.Chase);
                }

            break;     
            case AIState.Chase:
                //chase the player
                DoChaseState();
                if(!IsDistanceLessThan(target, targetDistance + 3))
                {
                    pawn.moveSpeed = pawn.moveSpeed * 3;
                    ChangeState(AIState.Patrol);
                }
                else if (IsDistanceLessThan(target, targetDistance))
                {
                    ChangeState(AIState.Watching);
                }
            break;
        }
    }
}
