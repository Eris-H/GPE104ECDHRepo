using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<Mover>();

    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void MoveFor()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBack()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClock()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounter()
    {
        mover.Rotate(-turnSpeed);
    }   

}
