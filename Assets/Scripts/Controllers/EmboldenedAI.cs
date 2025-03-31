using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmboldenedAI : AIController
{
    //will run away from the player, until they've taken damage, then they'll rush at the player until they die

    public override void Start()
    {
        base.Start();
        ChangeState(AIState.Flee);
        //Health myHealth = pawn.gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //Debug.Log(myHealth.currentHealth);
        //Debug.Log(pawn.health.currentHealth);

    }

     public override void ProcessInputs()
    {
        switch (currentState)
        {
            //each case should be one of the options in the enum
            case AIState.Guard:
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                    DoGuardState();

                    pawn.RotateTowards(target.transform.position);

                    if (IsDistanceLessThan(target, targetDistance))
                    {
                        ChangeState(AIState.Flee);
                    }
                }
                else
                {
                    TargetNearestPlayer();
                }

                break;
            case AIState.Flee:
                //find target
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                    //do thing
                    DoFleeState();

                    if (pawn.health.currentHealth < pawn.health.maxHealth - 24)
                    {
                        ChangeState(AIState.Attack);
                    }
                    else if (!IsDistanceLessThan(target, targetDistance))
                    {
                        ChangeState(AIState.Guard);
                    }
                }
                else
                {
                    //TargetPlayerOne();
                    TargetNearestPlayer();

                }
                //check for transition

                
                
            break;
            case AIState.Attack:
                TargetNearestPlayer();

                if (IsHasTarget())
                {
                    DoAttackState();
                    //do thing
                }
                else
                {
                    //TargetPlayerOne();
                    TargetNearestPlayer();

                }
                break;     
            
        }
    }

}
