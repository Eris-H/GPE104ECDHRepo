using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OptionsScreenStateObject = this.gameObject;
        }
    }
}
