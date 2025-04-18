using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Guard, Chase, Attack, Flee, Patrol, Watching };
    public AIState currentState;

    public GameObject target;

    //for patrol
    public Transform[] waypoints;
    public float waypointStopDistance;

    private int currentWaypoint = 0;


    public float targetDistance;

    private float lastStageChangeTime;

    public float fleeDistance;

    public float fieldOfView;
    
    public float hearingDistance;


    // Start is called before the first frame update
    public override void Start()
    {

        ChangeState(AIState.Guard);
        //ChangeState(AIState.Patrol);
        lives = 1;

    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
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
                    TargetNearestPlayer();
                }
                //check for transition
                //look
                if (IsCanSee(target))
                {
                    ChangeState(AIState.Chase);
                }
                if (IsDistanceLessThan(target, targetDistance))
                {
                    //change into chase
                    ChangeState(AIState.Chase);
                }
                //listen
                //if (IsCanHear(target))
                //{
                //    ChangeState(AIState.Attack);
                //}
            break;
            case AIState.Chase:
                //do thing
                DoChaseState();
                //check for transition
                if (!IsDistanceLessThan(target, targetDistance))
                {
                    //change into guard
                    ChangeState(AIState.Guard);
                }
                else if (IsDistanceLessThan(target, targetDistance / 2))
                {
                    //change into chase
                    ChangeState(AIState.Attack);
                }
            break;
            case AIState.Attack:
                //chase then shoot
                DoAttackState();
                //if player got away
                if (!IsDistanceLessThan(target, targetDistance / 2))
                {
                    //change into chase
                    ChangeState(AIState.Chase);
                }
                else if (IsDistanceLessThan(target, targetDistance / 4))
                {
                    //double speed for flee, good scurrying effect
                    pawn.moveSpeed = pawn.moveSpeed * 3;
                    ChangeState(AIState.Flee);
                }
            break;
            case AIState.Flee:
                //RUN AWAY!
                DoFleeState();
                if (!IsDistanceLessThan(target, targetDistance))
                {
                    //half speed for normal
                    pawn.moveSpeed = pawn.moveSpeed / 3;
                    ChangeState(AIState.Guard);
                }
            break;
            case AIState.Patrol:
                //do work for patrol
                DoPatrolState();
                //check for transition

            break;
            case AIState.Watching:
                //chase then shoot
                DoWatchState();
                //if player got away
                if (!IsDistanceLessThan(target, targetDistance / 2))
                {
                    //change into chase
                    ChangeState(AIState.Chase);
                }
                else if (IsDistanceLessThan(target, targetDistance / 4))
                {
                    //double speed for flee, good scurrying effect
                    pawn.moveSpeed = pawn.moveSpeed * 3;
                    ChangeState(AIState.Flee);
                }
            break;
                
        }
    }

    protected void DoPatrolState()
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


        Debug.Log("doing guard state");
    }

    public void DoChaseState()
    {
        //seek target 
        Debug.Log("doing chase state");
        if (target != null)
        {
            Seek(target);
        }
    }

    public void Shoot()
    {
        pawn.Shoot();
    }

    public virtual void DoAttackState()
    {
        if (target != null)
        {
            //seek then shoot
            Seek(target);
            Shoot();
        }
    }

    public virtual void DoWatchState()
    {
        if (target != null)
        {
            //look at player
            pawn.RotateTowards(target.transform.position);
            Shoot();
        }

    }


    public virtual void DoFleeState()
    {
        if (target != null)
        {
            Flee();
        }
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

    protected void TargetNearestTank()
    {
        //list of every tank
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        
        //assume first tank is closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        //check each one, one at a time
        foreach (Pawn tank in allTanks)
        {
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        //target closest
        target = closestTank.gameObject;
    }


    //it's manual and doesn't account for more than two players, but two players are all that we have
    public void TargetNearestPlayer()
    {
        //list every player
        if (GameManager.instance.players.Count > 0)
        {
            //run the check for closer player if there's more than one
            if (GameManager.instance.players.Count > 1)
            {
                Debug.Log("looking for two");

                //collect both players
                GameObject Play1 = GameManager.instance.players[0].pawn.gameObject;
                GameObject Play2 = GameManager.instance.players[1].pawn.gameObject;
            

                //assume closest is player 1
                GameObject closestPlay = Play1;

                if (Play1 != null && Play2 != null)
                {
                    //check which player is closer, priority player 1
                    if (Vector3.Distance(pawn.transform.position, Play1.transform.position) <= Vector3.Distance(pawn.transform.position, Play2.transform.position))
                    {
                        closestPlay = Play1;
                    }
                    else 
                    {
                        closestPlay = Play2;
                    }           
            
                    target = closestPlay.gameObject;
                    Debug.Log(target.name);
                }
                else
                {
                    Debug.Log("can't find");
                }


            }
            else
            {
                Debug.Log("looking for one");
                //run the same code as target player one if there's only one
                
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
        //Debug.Log(angleToTarget);
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
    }

    public override void AddToScore(int scoreGained)
    {

    }
    /*public override void Respawn()
    {

    }*/
}
