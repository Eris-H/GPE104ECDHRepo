using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    //holds the pawn
    public Pawn pawn;
    //score
    public int score;

    public int lives;

    public int playerID;



    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void ProcessInputs();
    public abstract void AddToScore(int scoreGained);
    //public abstract void Respawn();
}
