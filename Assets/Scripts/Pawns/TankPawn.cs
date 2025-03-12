using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    //fire rate
    private float timerDelay;    
    private float nextShootTime;

    // Start is called before the first frame update
    public override void Start()
    {
        timerDelay = 1 / fireRate;
        nextShootTime = Time.time + nextShootTime;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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

    public override void Shoot()
    {
        if (Time.time >= nextShootTime)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, shellLifespan);
            nextShootTime = Time.time + timerDelay;
        }

    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        //find targets vector
        Vector3 vectorToTarget = targetPosition - transform.position;
        //find rotation to look at it
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);   
        //rotate to the vector but not over turnSpeed
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void MakeNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = noiseMakerVolume;
        }
    }

    public override void StopNoise()
    {
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 0;
        }
    }

}
