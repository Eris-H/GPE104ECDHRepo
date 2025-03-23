using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //move speed
    public float moveSpeed;
    //turn speed
    public float turnSpeed;

    //for the shooter
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;
    //shots per second
    public float fireRate;

    public Mover mover;

    public Shooter shooter;

    public Health health;

    public NoiseMaker noiseMaker;
    public float noiseMakerVolume;

    //holds the controller
    public Controller controller;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

        health = GetComponent<Health>();

        noiseMaker = GetComponent<NoiseMaker>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveFor();
    public abstract void MoveBack();
    public abstract void RotateClock();
    public abstract void RotateCounter();
    public abstract void Shoot();

    public abstract void RotateTowards(Vector3 targetPosition);

    public abstract void MakeNoise();
    public abstract void StopNoise();
}
