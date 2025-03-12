using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{

    public KeyCode moveForKey;
    public KeyCode moveBackKey;
    public KeyCode RotateClockKey;
    public KeyCode RotateCounterKey;
    public KeyCode shootKey;

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
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }

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

}
