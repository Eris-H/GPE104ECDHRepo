using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardAI : AIController
{
    //will fight like normal AI
    //if player gets close, approach and when close enough, attack
    //flee if player is too close
    //however if they take too much damage, they'll run away indefinitely


    public bool scared;
    
    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Guard);
        scared = false;
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
            case AIState.Flee:
                //find target
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                    DoFleeState();
                    if (scared == false)
                    {
                        if (!IsDistanceLessThan(target, targetDistance))
                        {
                            pawn.moveSpeed = pawn.moveSpeed / 3;
                            ChangeState(AIState.Guard);
                        }
                    }
                }
                else
                {
                    TargetNearestPlayer();
                }


                break;
            case AIState.Attack:
                //do thing
                TargetNearestPlayer();

                DoAttackState();
                if (target != null)
                {
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
                    if (pawn.health.currentHealth < pawn.health.maxHealth - 74)
                    {
                        scared = true;
                        ChangeState(AIState.Flee);
                    }
                }
                else
                {
                    TargetNearestPlayer();
                }
            break;     
            case AIState.Guard:
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                    DoGuardState();

                    //do thing
                    DoGuardState();
                    pawn.RotateTowards(target.transform.position);
                    
                    if (pawn.health.currentHealth < pawn.health.maxHealth - 74)
                    {
                        scared = true;
                        pawn.moveSpeed = pawn.moveSpeed * 3;
                        ChangeState(AIState.Flee);
                    }
                    else if (IsDistanceLessThan(target, targetDistance))
                    {
                        ChangeState(AIState.Chase);
                    }
                    else if (IsDistanceLessThan(target, targetDistance / 4))
                    {
                        //double speed for flee, good scurrying effect
                        pawn.moveSpeed = pawn.moveSpeed * 3;
                        ChangeState(AIState.Flee);
                    }
                }
                else
                { 
                    //TargetNearestTank();
                    //TargetPlayerOne(); 
                    TargetNearestPlayer();

                }
            break;
            case AIState.Chase:
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                        DoChaseState();

                        if (pawn.health.currentHealth < pawn.health.maxHealth - 74)
                        {
                            ChangeState(AIState.Flee);
                        }
                        else if (!IsDistanceLessThan(target, targetDistance))
                        {
                            //change into guard
                            ChangeState(AIState.Guard);
                        }
                        else if (IsDistanceLessThan(target, targetDistance / 2))
                        {
                            //change into chase
                            ChangeState(AIState.Attack);
                        }
                }
                else
                {
                    TargetNearestPlayer();
                }             
            break;
        }
    }
}
