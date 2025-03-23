using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.CreditsScreenStateObject = this.gameObject;
        }
    }

}
