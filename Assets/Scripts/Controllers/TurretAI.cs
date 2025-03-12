using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : AIController
{
    //will wait until they see or hear player
    //once they see player, they'll shoot at them until they can't see them
    //if they can't see them but they're still close enough, they'll spin around to look, for them


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
            case AIState.Guard:
                //find target
                if (IsHasTarget())
                {
                    //do thing
                    DoGuardState();
                }
                else
                {
                    TargetPlayerOne();
                }
                //check for transition
                //look
                if (IsCanSee(target))
                {
                    ChangeState(AIState.Watching);
                }
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
                if(!IsDistanceLessThan(target, targetDistance) && !IsCanSee(target))
                {
                    //change into guard
                    ChangeState(AIState.Guard);
                }
                
                //else if (!IsCanHear(target))
                //{
                    //change into chase
                //    ChangeState(AIState.Guard);
                //}
            break;     
        }
    }

    /*protected void DoPatrolState()
    {
        //if enough waypoints to move
        if(waypoints.Length > currentWaypoint)
        {
            //seek it 
            Seek(waypoints[currentWaypoint]);
            //if close enough, then next waypoint
            if(Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else 
        {
            RestartPatrol();
        }
    } 

    protected void RestartPatrol()
    {
        //set index to 0
        currentWaypoint = 0;
    }

    public void DoGuardState()
    {
        //spin in place until something happens
        //not while testing fov
        //pawn.RotateTowards(target.transform.position);

        Debug.Log("doing guard state");
    }

    public void DoChaseState()
    {
        //seek target 
        Debug.Log("doing chase state");
        Seek(target);
    }

    public void Shoot()
    {
        pawn.Shoot();
    }

    public virtual void DoAttackState()
    {
        //seek then shoot
        Seek(target);
        Shoot();
    }

    public virtual void DoFleeState()
    {
        Flee();
    }

    protected void Flee()
    {        
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;
        Seek(pawn.transform.position + fleeVector);
    }   

    
    public void Seek (GameObject target)
    {
        //do rotate towards target
        pawn.RotateTowards(target.transform.position);
        //move to target
        pawn.MoveFor();
    }
    //overloading
    public void Seek (Vector3 targetPosition)
    {
        pawn.RotateTowards(targetPosition);
        pawn.MoveFor();
    } 
    public void Seek (Transform targetTransform)
    {
        Seek(targetTransform.position);
    }
    public void Seek (Pawn targetPawn)
    {
        //do rotate towards target
        Seek(targetPawn.transform);
    }

    public bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //changes state to specified
    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;

        lastStageChangeTime = Time.time;
    }

    public void TargetPlayerOne()
    {
        //if game manager exists
        if (GameManager.instance != null)
        {
            //and has players
            if (GameManager.instance.players.Count > 0)
            {
                //target the first
                target = GameManager.instance.players[0].pawn.gameObject;
            }
        }
    }

    protected bool IsHasTarget()
    {
        //will be true if we have target
        return (target != null);
    }

    protected bool IsCanHear(GameObject target)
    {
        //get target noisemaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();

        //if no maker, return false
        if (noiseMaker == null)
        {
            return false;
        }
        //if making no noise can't hear
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        //if making noise, check if we can hear
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        //if distance between pawn and target is closer
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            //can hear target
            return true;
        }
        else
        {
            //we're too far
            return false;
        }
    }

    protected bool IsCanSee(GameObject target)
    {
        //find vector between target and agent
        Vector3 agentToTargetVector = target.transform.position - pawn.transform.position;
        //find the angle between where we're facing and targets position
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        Debug.Log(angleToTarget);
        //check if said above angle is within our fov restraint
        if(angleToTarget < fieldOfView)
        {
            Debug.Log("In field of view");
            return true;
        }
        else
        {
            return false;
        }
    }*/
}
