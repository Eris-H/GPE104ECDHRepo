using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{

    //move speed
    public float moveSpeed;
    //turn speed
    public float turnSpeed;

    public Mover mover;


    // Start is called before the first frame update
    public virtual void Start()
    {
        //mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveFor();
    public abstract void MoveBack();
    public abstract void RotateClock();
    public abstract void RotateCounter();
}
