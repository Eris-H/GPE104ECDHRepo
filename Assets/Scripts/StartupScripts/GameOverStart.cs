using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameOverStart : MonoBehaviour
{
    public Text P1SCORE;
    public Text P2SCORE;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.GameOverScreenStateObject = this.gameObject;
        }
    }
}
