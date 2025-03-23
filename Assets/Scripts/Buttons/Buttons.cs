using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void OpenMainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenu();
        }
    }

    public void StartTheGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerCount = 1;
            GameManager.instance.ActivateGameplayState();
        }
    }
    public void StartTheGame2PlayerMode()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerCount = 2;
            GameManager.instance.ActivateGameplayState();
        }
    }

    public void OpenOptions()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsScreen();
        }
    }
    public void IsSetSeed()
    {
        if (GameManager.instance.setSeed == false)
        {
            GameManager.instance.setSeed = true;
        }
        else
        {
            GameManager.instance.setSeed = false;
        }
    }

    public InputField inputSeed;

    public void ChangedMapSeed()
    {
        Debug.Log(inputSeed.text);
        //GameManager.instance.mapSeed = seedInput;
        GameManager.instance.mapSeed = int.Parse(inputSeed.text);
    }

    public void GameOverButton()
    {
        GameManager.instance.Restarted();
    }

    public void EndGameButton()
    {
        Application.Quit();
    }
}
