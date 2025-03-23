using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TankPawn : Pawn
{
    //fire rate
    private float timerDelay;    
    private float nextShootTime;

    private float oldFirerate;

    public AudioClip firingSound;
    AudioSource audioSource;


    // Start is called before the first frame update
    public override void Start()
    {
        timerDelay = 1 / fireRate;
        nextShootTime = Time.time + nextShootTime;
        oldFirerate = fireRate;
        base.Start();

        audioSource = GetComponent<AudioSource>();
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
            //FiringAudio = GetComponent<AudioSource>();
            //FiringAudio.Play();
            audioSource.PlayOneShot(firingSound, 0.7f);
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
    
    public void UpdateFirerate(float newFirerate)
    {
        if (fireRate != oldFirerate)
        {
            fireRate = oldFirerate;
            timerDelay = 1 / fireRate;
        }
        else
        {
            fireRate = newFirerate;
            timerDelay = 1 / fireRate;
        }
        
    }

    public void OnDestroy()
    {

        if (controller.lives != 0)
        {
            controller.lives --;
            //controller.Respawn();
            GameManager.instance.Respawn(this);
        }
        else if (controller.lives == 0)
        {
            Destroy(controller.gameObject);

            GameManager.instance.GameEnd();

            /*if (controller.playerID != 0)
            {
                Pawn[] allTanks = FindObjectsOfType<Pawn>();

                foreach (Pawn tank in allTanks)
                {
                    tank.controller.lives = 0;
                    Controller Control = tank.controller.GetComponent<Controller>();
                    Destroy(Control.gameObject);
                    Destroy(tank.gameObject);
                }

            }*/
        }
    }
}
