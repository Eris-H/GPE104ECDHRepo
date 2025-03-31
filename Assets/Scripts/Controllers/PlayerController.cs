using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{

    public KeyCode moveForKey;
    public KeyCode moveBackKey;
    public KeyCode RotateClockKey;
    public KeyCode RotateCounterKey;
    public KeyCode shootKey;

    public KeyCode P2moveForKey;
    public KeyCode P2moveBackKey;
    public KeyCode P2RotateClockKey;
    public KeyCode P2RotateCounterKey;
    public KeyCode P2shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this);
            }
        }
        base.Start();
        lives = 3;

        if (playerID == 2)
        {
            moveForKey = P2moveForKey;
            moveBackKey = P2moveBackKey;
            RotateClockKey = P2RotateClockKey;
            RotateCounterKey = P2RotateCounterKey;
            shootKey = P2shootKey;
        }
    }

    public void OnDestroy()
    {
            if (GameManager.instance != null)
            {
                Debug.Log("I died");

                if (GameManager.instance.players != null)
                {
                    GameManager.instance.players.Remove(this);
                }
            }
    }

    /*public override void Respawn()
    {
        if(lives != 0)
        {
            SpawnPlayer();
            Pawn[] allTanks = FindObjectsOfType<Pawn>();
        }
    }*/

    //public override void TargetNearestPlayer(){}

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();

        base.Update();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForKey))
        {
            pawn.MoveFor();
            pawn.MakeNoise();
        }

        if (Input.GetKey (moveBackKey))
        {
            pawn.MoveBack();
            pawn.MakeNoise();
        }

         if (Input.GetKey(RotateClockKey))
        {
            pawn.RotateClock();
            pawn.MakeNoise();
        }

        if (Input.GetKey (RotateCounterKey))
        {
            pawn.RotateCounter();
            pawn.MakeNoise();
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
            pawn.MakeNoise();
        }

        if (!Input.GetKey(moveForKey) && !Input.GetKey(moveBackKey) && !Input.GetKey(RotateClockKey) && !Input.GetKey(RotateCounterKey) && !Input.GetKeyDown(shootKey))
        {
            pawn.StopNoise();
        }
        
    }

    public Text currentScore;

    public override void AddToScore(int scoreGained)
    {
        score = scoreGained + score;
        currentScore.text = "SCORE: " + score;

        if (score >= 200)
        {
            GameManager.instance.GameEnd();
        }
    }

}
