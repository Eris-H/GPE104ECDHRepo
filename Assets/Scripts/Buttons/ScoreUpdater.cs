using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public Text currentScore;
    public int score;
    private Pawn ParentPawn;

    private void Start()
    {
        ParentPawn = GetComponentInParent<Pawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParentPawn != null)
        {
            score = ParentPawn.controller.score;

            currentScore.text = "SCORE: " + score;
        }
           
    }
}
